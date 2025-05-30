# Directory: Components\Pages\Suggestions

## File: Index.razor

```C#
@page "/mealsuggestion"
@rendermode InteractiveServer
@attribute [Authorize]

<TagList />

```

## File: TagList.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Interfaces
@using MyFoodApp.Application.Common
@using MyFoodApp.Web.Components.Shared
@inject IGenerateRecommendationsUseCases GenerateRecommendationsUseCases
@inject IGeneratorPdf PdfGenerator
@inject IJSRuntime JSRuntime

@code {
    private Response<MealSuggestionTagDto>? Response;
    private Dictionary<MealSuggestionTagDto, bool> _expandedState = new();
    private List<int> RecipeIdList = new();
    private Boolean isLoading = false;

    protected override async Task OnInitializedAsync()
    {
        if (Response == null)
        {
            await LoadMealSuggestions();
        }
    }

    private async Task LoadMealSuggestions()
    {
        Response = await GenerateRecommendationsUseCases.GetMealSuggestionWithRecipeListAsync();
        InitializeExpandedState();
        StateHasChanged();
    }

    private void InitializeExpandedState()
    {
        if (Response?.List != null)
        {
            foreach (var mealTag in Response.List)
            {
                if (!_expandedState.ContainsKey(mealTag))
                {
                    _expandedState[mealTag] = true;
                }
            }
        }
    }

    private async Task AddRecipeFromList(int id)
    {
        RecipeIdList.Add(id);
    }

    private async Task RemoveRecipeFromList(int id)
    {
        RecipeIdList.Remove(id);
    }

    private async Task DownloadPdf()
    {
        isLoading = true;
        var pdfBytes = await PdfGenerator.GenerateRecipeListPdfAsync(RecipeIdList);
        var pdfBase64 = Convert.ToBase64String(pdfBytes);
        await JSRuntime.InvokeVoidAsync("downloadFile", "recipe_document.pdf", "application/pdf", pdfBase64);
        isLoading = false;
    }
}

<div class="container">
    <h1 class="my-4 text-center">Meal Suggestion</h1>
    @if (RecipeIdList.Count > 0)
    {
        <button class="btn btn-secondary mb-3" @onclick="DownloadPdf">Download PDF</button>
    }

    @if(isLoading)
    {
        <div class="d-flex justify-content-center my-5">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    }
</div>

<div class="container">
    @if (Response == null)
    {
        <div class="d-flex justify-content-center my-5">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    }
    else if (Response.ErrorList.Any())
    {
        <DisplayError Errors="Response.ErrorList" />
    }
    else if (Response.List.Any() == false)
    {
        <div class="text-center py-5">
            <i class="bi bi-journal-x fs-1 text-muted"></i>
            <p class="text-muted mt-2">No meal suggestion found</p>
        </div>
    }
    else
    {
        <div class="meal-tags-container">
            @foreach (var mealTag in Response.List)
            {
                <div class="card mb-4 shadow-lg">
                    <div class="card-header bg-light d-flex justify-content-between align-items-center">
                        <button class="btn btn-link text-dark text-decoration-none p-0"
                                @onclick="@(() => _expandedState[mealTag] = !_expandedState[mealTag])">
                            <i class="fas @(_expandedState[mealTag] ? "fa-chevron-down" : "fa-chevron-right") me-2"></i>
                            @mealTag.TagName (@mealTag.MealSuggestions.Count)
                        </button>
                    </div>

                    <div class="collapse @(_expandedState[mealTag] ? "show" : "")">
                        <div class="card-body p-3">
                            <div class="row g-4">
                                @foreach (var mealSuggestion in mealTag.MealSuggestions)
                                {
                                    <div class="col-12">
                                        <div class="card shadow-sm h-100">
                                            <div class="card-header bg-light">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <h5 class="card-title mb-0 text-primary">@mealSuggestion.Name</h5>
                                                    <small class="text-muted">
                                                        @mealSuggestion.EffectiveDate.ToShortDateString() -
                                                        @((mealSuggestion.ExpirationDate ?? DateTime.UtcNow).ToShortDateString())
                                                    </small>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="mb-3">
                                                    <p class="mb-1"><strong class="badge bg-primary bg-opacity-10 text-primary">@mealSuggestion.MealType</strong> @mealSuggestion.Description </p>
                                                </div>

                                                <h6 class="mt-4 mb-3 text-muted">Recipes</h6>
                                                <div class="row g-4">
                                                    @foreach (var recipeSuggestion in mealSuggestion.RecipeSuggestions)
                                                    {
                                                        <div class="col-lg-4 col-md-6">
                                                            <div class="card h-100 shadow-sm">
                                                                <div class="card-body">
                                                                    @if (recipeSuggestion.Recipe != null)
                                                                    {
                                                                        <h5 class="card-title fs-6">@recipeSuggestion.Recipe.Title</h5>
                                                                        if (!RecipeIdList.Contains(recipeSuggestion.Recipe.RecipeId))
                                                                        {
                                                                            <button class="btn btn-primary" @onclick="() => AddRecipeFromList(recipeSuggestion.Recipe.RecipeId)">Add Recipe Download</button>
                                                                        }
                                                                        else
                                                                        {
                                                                            <button class="btn btn-danger" @onclick="() => RemoveRecipeFromList(recipeSuggestion.Recipe.RecipeId)">Remove Recipe Download</button>
                                                                        }
                                                                        <p class="card-text text-muted small">@recipeSuggestion.Recipe.Description</p>
                                                                        <div class="d-flex justify-content-between small text-muted mb-3">
                                                                            <span>
                                                                                <i class="bi bi-clock"></i>
                                                                                @recipeSuggestion.Recipe.CookTimeMinutes mins
                                                                            </span>
                                                                            <span>
                                                                                <i class="bi bi-prep"></i>
                                                                                @recipeSuggestion.Recipe.PrepTimeMinutes mins
                                                                            </span>
                                                                        </div>
                                                                        <div class="ingredients">
                                                                            @foreach (var ingredient in recipeSuggestion.Recipe.Ingredients)
                                                                            {
                                                                                @if (ingredient.FoodItem != null)
                                                                                {
                                                                                    <span class="badge bg-light text-dark border me-1 mb-1 small">
                                                                                        @ingredient.FoodItem.Name
                                                                                    </span>
                                                                                }
                                                                            }
                                                                        </div>
                                                                    }
                                                                </div>
                                                            </div>
                                                        </div>
                                                    }
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                }
                            </div>
                        </div>
                    </div>
                </div>
            }
        </div>
    }
</div>

```

