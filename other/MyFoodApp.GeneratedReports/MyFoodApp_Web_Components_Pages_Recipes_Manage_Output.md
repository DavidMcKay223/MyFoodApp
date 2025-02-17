# Directory: Components\Pages\Recipes\Manage

## File: EditModal.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Interfaces
@using MyFoodApp.Application.Validators
@using MyFoodApp.Application.Common
@using MyFoodApp.Web.Components.Shared
@using FluentValidation
@using Blazored.FluentValidation
@inject IRecipeUseCases RecipeUseCases
@inject IFoodItemUseCases FoodItemUseCases
@inject IMealSuggestionUseCases MealSuggestionUseCases
@inject IValidator<RecipeDto> RecipeValidator

@code {
    [Parameter]
    public EventCallback OnRecipeSaved { get; set; }

    private List<FoodItemDto> FoodItemList { get; set; } = [];
    private List<MealSuggestionDto> MealSuggestionList { get; set; } = [];

    private EditForm? EditForm;
    private RecipeDto? Recipe;
    private FluentValidationValidator? _fluentValidationValidator;
    private List<Error> Errors { get; set; } = new();

    public async void Show(RecipeDto? recipeToEdit)
    {
        Errors.Clear();
        Recipe = recipeToEdit ?? new RecipeDto()
        {
            Title = "",
            Description = "",
            Steps = new List<RecipeStepDto>(),
            Ingredients = new List<IngredientDto>(),
            MealSuggestions = new List<RecipeMealSuggestionDto>()
        };

        var response1 = await FoodItemUseCases.GetFoodItemListAsync();
        FoodItemList = response1.List;

        var response2 = await MealSuggestionUseCases.GetMealSuggestionListAsync();
        MealSuggestionList = response2.List;

        StateHasChanged();
    }

    private async Task SaveRecipe()
    {
        Errors.Clear();
        if (await _fluentValidationValidator!.ValidateAsync())
        {
            Response<RecipeDto>? response = null;
            try
            {
                if (Recipe!.RecipeId == 0)
                {
                    response = await RecipeUseCases.CreateRecipeAsync(Recipe);
                }
                else
                {
                    response = await RecipeUseCases.UpdateRecipeAsync(Recipe.RecipeId, Recipe);
                }

                if (response.ErrorList.Any())
                {
                    Errors = response.ErrorList;
                    StateHasChanged();
                }
                else
                {
                    Recipe = null;
                    await OnRecipeSaved.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                Errors = new List<Error> { new() { Code = "ERROR", Message = ex.Message } };
                StateHasChanged();
            }
        }
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
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content" style="height: 80%;">
                    <div class="modal-header">
                        <h5 class="modal-title">@(Recipe.RecipeId == 0 ? "Add Recipe" : "Edit Recipe")</h5>
                        <button type="button" class="btn-close" @onclick="Cancel"></button>
                    </div>
                    <div class="modal-body">
                        <EditForm @ref="EditForm" Model="@Recipe">
                            <FluentValidationValidator @ref="_fluentValidationValidator" />
                            <DataAnnotationsValidator />
                            
                            <div class="form-floating mb-3">
                                <InputText @bind-Value="Recipe.Title" class="form-control" />
                                <label>Title</label>
                                <ValidationMessage For="@(() => Recipe.Title)" />
                            </div>
                            
                            <div class="form-floating mb-3">
                                <InputTextArea @bind-Value="Recipe.Description" class="form-control" />
                                <label>Description</label>
                                <ValidationMessage For="@(() => Recipe.Description)" />
                            </div>
                            
                            <div class="row g-3 mb-3">
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <InputNumber @bind-Value="Recipe.PrepTimeMinutes" class="form-control" />
                                        <label>Prep Time (minutes)</label>
                                        <ValidationMessage For="@(() => Recipe.PrepTimeMinutes)" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <InputNumber @bind-Value="Recipe.CookTimeMinutes" class="form-control" />
                                        <label>Cook Time (minutes)</label>
                                        <ValidationMessage For="@(() => Recipe.CookTimeMinutes)" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <InputNumber @bind-Value="Recipe.Servings" class="form-control" />
                                        <label>Servings</label>
                                        <ValidationMessage For="@(() => Recipe.Servings)" />
                                    </div>
                                </div>
                            </div>

                            <!-- Recipe Steps -->
                            <MyFoodApp.Web.Components.Pages.Recipes.Steps.Edit Steps="@Recipe.Steps" />

                            <!-- Ingredients -->
                            <MyFoodApp.Web.Components.Pages.Recipes.Ingredients.Edit Ingredients="@Recipe.Ingredients" FoodItemList="FoodItemList" />

                            <!-- Meal Suggestions -->
                            <MyFoodApp.Web.Components.Pages.Recipes.MealSuggestions.Edit MealSuggestions="@Recipe.MealSuggestions" MealSuggestionList="MealSuggestionList" />
                        </EditForm>

                        <DisplayError Errors="Errors" />
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary" @onclick="SaveRecipe">Save</button>
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
@page "/recipes-edit"
@rendermode InteractiveServer
@* @attribute [Authorize(Roles = "Admin")] *@

<MyFoodApp.Web.Components.Pages.Recipes.Manage.List />

```

## File: List.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Application.Interfaces
@using MyFoodApp.Application.Common
@using MyFoodApp.Web.Components.Shared
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

    <MyFoodApp.Web.Components.Pages.Recipes.Manage.Table Recipes="Recipes" OnEdit="ShowEditModal" OnDelete="DeleteRecipe" />

    <SearchPagination TItem="RecipeDto" TotalItems="@totalItems" PageSize="@pageSize" CurrentPage="@currentPage" OnPageChanged="OnPageChanged" OnPageSizeChangedCallback="OnPageSizeChanged" />

    <DisplayError Errors="Errors" />
}

<EditModal @ref="editModal" OnRecipeSaved="LoadRecipes" />

@code {
    private List<RecipeDto>? Recipes;
    private List<Error> Errors { get; set; } = new();
    private bool isLoading = true;
    private EditModal? editModal;
    private RecipeSearchDto searchDto = new();
    private int totalItems;
    private int pageSize = 10;
    private int currentPage = 0;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadRecipes();
        }
        catch (Exception ex)
        {
            Errors.Add(new Error { Code = "InitError", Message = ex.Message });
            isLoading = false;
            StateHasChanged();
        }
    }

    private async Task LoadRecipes()
    {
        try
        {
            isLoading = true;
            Errors.Clear();
            searchDto.PageNumber = currentPage;
            searchDto.PageSize = pageSize;

            var response = await RecipeUseCases.LookupRecipesAsync(searchDto);

            if (response.ErrorList.Any())
            {
                Errors.AddRange(response.ErrorList);
            }
            else
            {
                Recipes = response.List;
                totalItems = response.TotalItems;
            }
        }
        catch (Exception ex)
        {
            Errors.Add(new Error { Code = "LoadError", Message = ex.Message });
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }

    private void ShowAddModal()
    {
        try
        {
            Errors.Clear();
            editModal!.Show(null);
        }
        catch (Exception ex)
        {
            Errors.Add(new Error { Code = "AddModalError", Message = ex.Message });
            StateHasChanged();
        }
    }

    private async Task ShowEditModal(int id)
    {
        try
        {
            Errors.Clear();
            var response = await RecipeUseCases.GetRecipeByIdAsync(id);

            if (response.ErrorList.Any())
            {
                Errors.AddRange(response.ErrorList);
            }
            else if (response.Item != null)
            {
                editModal!.Show(response.Item);
            }
            else
            {
                Errors.Add(new Error { Code = "NotFound", Message = "Recipe not found" });
            }
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Errors.Add(new Error { Code = "EditModalError", Message = ex.Message });
            StateHasChanged();
        }
    }

    private async Task DeleteRecipe(RecipeDto dto)
    {
        try
        {
            Errors.Clear();
            var response = await RecipeUseCases.DeleteRecipeAsync(dto.RecipeId);

            if (response.ErrorList.Any())
            {
                Errors.AddRange(response.ErrorList);
            }
            else
            {
                await LoadRecipes();
            }
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Errors.Add(new Error { Code = "DeleteError", Message = ex.Message });
            StateHasChanged();
        }
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

## File: Table.razor

```C#
@using MyFoodApp.Application.DTOs

@if (Recipes?.Any() == false)
{
    <p class="text-muted">No recipes found.</p>
}
else
{
    <table class="table table-striped table-hover">
        <thead>
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Prep Time</th>
                <th>Cook Time</th>
                <th>Servings</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var recipe in Recipes!)
            {
                <tr>
                    <td>@recipe.Title</td>
                    <td>@recipe.Description</td>
                    <td>@recipe.PrepTimeMinutes mins</td>
                    <td>@recipe.CookTimeMinutes mins</td>
                    <td>@recipe.Servings</td>
                    <td>
                        <button class="btn btn-primary btn-sm"
                                @onclick="() => OnEdit.InvokeAsync(recipe.RecipeId)">
                            Edit
                        </button>
                        <button class="btn btn-danger btn-sm"
                                @onclick="() => OnDelete.InvokeAsync(recipe)">
                            Delete
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    [Parameter]
    public List<RecipeDto>? Recipes { get; set; }

    [Parameter]
    public EventCallback<int> OnEdit { get; set; }

    [Parameter]
    public EventCallback<RecipeDto> OnDelete { get; set; }
}

```

