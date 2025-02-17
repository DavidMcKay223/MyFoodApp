# Directory: Components\Pages\Recipes

## File: DisplayModal.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Common
@using MyFoodApp.Web.Components.Shared

@code {
    private RecipeDto? Recipe;

    public void Show(RecipeDto recipeToDisplay)
    {
        Recipe = recipeToDisplay;
        StateHasChanged();
    }

    private void Cancel()
    {
        Recipe = null;
    }
}

@if (Recipe != null)
{
    <div class="modal-backdrop">
        <div class="modal show d-block" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered modal-lg shadow-lg">
                <div class="modal-content" style="height: 80%;">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title">
                            <i class="fas fa-book-open me-2"></i> Recipe Details
                        </h5>
                        <button type="button" class="btn-close btn-close-white" @onclick="Cancel"></button>
                    </div>
                    <div class="modal-body">
                        <h5 class="card-title">@Recipe.Title</h5>
                        <p class="card-text">@Recipe.Description</p>

                        <div class="row g-3 mb-3">
                            <div class="col-md-4">
                                <p><strong>Prep Time (minutes):</strong> <span class="text-primary">@Recipe.PrepTimeMinutes</span></p>
                            </div>
                            <div class="col-md-4">
                                <p><strong>Cook Time (minutes):</strong> <span class="text-primary">@Recipe.CookTimeMinutes</span></p>
                            </div>
                            <div class="col-md-4">
                                <p><strong>Servings:</strong> <span class="text-primary">@Recipe.Servings</span></p>
                            </div>
                        </div>

                        <!-- Recipe Steps -->
                        <MyFoodApp.Web.Components.Pages.Recipes.Steps.Display Steps="@Recipe.Steps" />

                        <!-- Ingredients -->
                        <MyFoodApp.Web.Components.Pages.Recipes.Ingredients.Display Ingredients="@Recipe.Ingredients" />

                        <!-- Meal Suggestions -->
                        <MyFoodApp.Web.Components.Pages.Recipes.MealSuggestions.Display MealSuggestions="@Recipe.MealSuggestions" RecipeId="@Recipe.RecipeId" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @onclick="Cancel">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
}

```

## File: Index.razor

```C#
@page "/recipes"
@rendermode InteractiveServer
@attribute [Authorize]

<List />

```

## File: List.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Interfaces
@using MyFoodApp.Application.Common
@using MyFoodApp.Web.Components.Shared
@using MyFoodApp.Web.Components.Pages.Recipes.Manage
@inject IRecipeUseCases RecipeUseCases

<div class="container">
    <h1 class="my-4 text-center">Recipes</h1>
</div>

@if (isLoading)
{
    <div class="d-flex justify-content-center my-5">
        <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    </div>
}
else
{
    <Search SearchDto="searchDto" />

    <SearchButton OnSearch="Search" OnReset="ResetSearch" OnAdd="ShowAddModal" />

    <MyFoodApp.Web.Components.Pages.Recipes.Table Recipes="Recipes" OnEdit="ShowEditModal" OnDisplay="ShowDisplayModal" />

    <SearchPagination TItem="RecipeDto" TotalItems="@totalItems" PageSize="@pageSize" CurrentPage="@currentPage" OnPageChanged="OnPageChanged" OnPageSizeChangedCallback="OnPageSizeChanged" />
}

<DisplayModal @ref="displayModal" />

<EditModal @ref="editModal" OnRecipeSaved="LoadRecipes" />

@code {
    private List<RecipeDto>? Recipes;
    private bool isLoading = true;
    private EditModal? editModal;
    private DisplayModal? displayModal;
    private RecipeSearchDto searchDto = new();
    private int totalItems;
    private int pageSize = 10;
    private int currentPage = 0;

    protected override async Task OnInitializedAsync()
    {
        await LoadRecipes();
    }

    private async Task LoadRecipes()
    {
        isLoading = true;
        searchDto.PageNumber = currentPage;
        searchDto.PageSize = pageSize;

        var response = await RecipeUseCases.LookupRecipesAsync(searchDto);
        Recipes = response.List;
        totalItems = response.TotalItems;
        isLoading = false;
        StateHasChanged();
    }

    private void ShowAddModal()
    {
        editModal!.Show(null);
    }

    private async void ShowEditModal(int id)
    {
        var response = await RecipeUseCases.GetRecipeByIdAsync(id);
        if (response.Item != null)
        {
            editModal!.Show(response.Item);
        }
    }

    private async void ShowDisplayModal(int id)
    {
        var response = await RecipeUseCases.GetRecipeByIdAsync(id);
        if (response.Item != null)
        {
            displayModal!.Show(response.Item);
        }
    }

    private async Task DeleteRecipe(RecipeDto dto)
    {
        await RecipeUseCases.DeleteRecipeAsync(dto.RecipeId);
        await LoadRecipes();
    }

    private async Task OnPageChanged(int page)
    {
        currentPage = page;
        await LoadRecipes();
    }

    private async Task OnPageSizeChanged(int size)
    {
        pageSize = size;
        currentPage = 0;
        await LoadRecipes();
    }

    private async Task Search()
    {
        currentPage = 0;
        await LoadRecipes();
    }

    private async Task ResetSearch()
    {
        searchDto = new RecipeSearchDto();
        await LoadRecipes();
    }
}

```

## File: Search.razor

```C#
@using MyFoodApp.Application.DTOs

<div class="row mb-3">
    <div class="col-md-4">
        <div class="mb-3 form-floating">
            <InputText @bind-Value="SearchDto.Title" class="form-control" />
            <label>Title</label>
        </div>
    </div>
    <div class="col-md-4">
        <div class="mb-3 form-floating">
            <InputText @bind-Value="SearchDto.Description" class="form-control" />
            <label>Description</label>
        </div>
    </div>
    <div class="col-md-2">
        <div class="mb-3 form-floating">
            <InputNumber @bind-Value="SearchDto.ServingsMin" class="form-control" />
            <label>Min Servings</label>
        </div>
    </div>
    <div class="col-md-2">
        <div class="mb-3 form-floating">
            <InputNumber @bind-Value="SearchDto.ServingsMax" class="form-control" />
            <label>Max Servings</label>
        </div>
    </div>
</div>

@code {
    [Parameter]
    public RecipeSearchDto SearchDto { get; set; } = new();
}

```

## File: Table.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Interfaces
@using MyFoodApp.Domain.Enums
@inject INutritionCalculatorService CalorieCalculatorService

@if (Recipes?.Any() == false)
{
    <div class="text-center py-5">
        <i class="bi bi-journal-x fs-1 text-muted"></i>
        <p class="text-muted mt-2">No recipes found</p>
    </div>
}
else
{
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
        @foreach (var recipe in Recipes!)
        {
            <div class="col">
                <div class="card h-100 shadow-sm hover-shadow transition-all">
                    <div class="card-body d-flex flex-column">
                        <div class="mb-3">
                            <h5 class="card-title fw-bold text-primary mb-3">@recipe.Title</h5>
                            <p class="card-text text-muted line-clamp-3">@recipe.Description</p>
                        </div>

                        <!-- Metadata Grid -->
                        <div class="row g-2 small mb-3">
                            <div class="col-6">
                                <div class="d-flex align-items-center text-muted">
                                    <i class="bi bi-list-ol me-2"></i>
                                    <span>@recipe.Steps.Count steps</span>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="d-flex align-items-center text-muted">
                                    <i class="bi bi-basket me-2"></i>
                                    <span>@recipe.Ingredients.Count ingredients</span>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="d-flex align-items-center text-muted">
                                    <i class="bi bi-fire me-2"></i>
                                    <span>@(CalorieCalculatorService.CalculateCalories(recipe)) cal/serving</span>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="d-flex align-items-center text-muted">
                                    <i class="bi bi-clock me-2"></i>
                                    <span>@(recipe.PrepTimeMinutes + recipe.CookTimeMinutes)m total</span>
                                </div>
                            </div>
                        </div>

                        <!-- Ingredients Preview -->
                        <div class="mb-3">
                            <h6 class="text-secondary small">Main Ingredients:</h6>
                            <div class="d-flex flex-wrap gap-2">
                                @foreach (var ingredient in recipe.Ingredients.Take(3))
                                {
                                    <span class="badge bg-light text-dark border">
                                        @ingredient.FoodItem?.Name
                                    </span>
                                }
                                @if (recipe.Ingredients.Count > 3)
                                {
                                    <span class="badge bg-light text-dark border">
                                        +@(recipe.Ingredients.Count - 3) more
                                    </span>
                                }
                            </div>
                        </div>

                        <!-- Meal Suggestions -->
                        @if (recipe.MealSuggestions.Any())
                        {
                            <div class="mt-auto">
                                <div class="d-flex flex-wrap gap-2">
                                    @foreach (var suggestion in recipe.MealSuggestions)
                                    {
                                        <span class="badge bg-primary bg-opacity-10 text-primary">
                                            @suggestion.MealSuggestion?.MealType - @suggestion.MealSuggestion?.Name
                                        </span>
                                    }
                                </div>
                            </div>
                        }
                    </div>

                    <!-- Footer with Actions -->
                    <div class="card-footer bg-transparent border-top-0">
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="text-muted small">
                                @recipe.Servings <i class="bi bi-people ms-1"></i>
                            </span>
                            <div class="mb-3">
                                @if (OnDisplay.HasDelegate)
                                {
                                    <button class="btn btn-outline-primary btn-sm me-2"
                                            @onclick="() => OnDisplay.InvokeAsync(recipe.RecipeId)">
                                        <i class="bi bi-view-list me-2"></i>View
                                    </button>
                                }
                                @if (OnEdit.HasDelegate)
                                {
                                    <button class="btn btn-outline-danger btn-sm me-2"
                                            @onclick="() => OnEdit.InvokeAsync(recipe.RecipeId)">
                                        <i class="bi bi-pencil me-2"></i>Edit
                                    </button>
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        }
    </div>
}

@code {
    [Parameter]
    public List<RecipeDto>? Recipes { get; set; }

    [Parameter]
    public EventCallback<int> OnEdit { get; set; }

    [Parameter]
    public EventCallback<int> OnDisplay { get; set; }
}

```

