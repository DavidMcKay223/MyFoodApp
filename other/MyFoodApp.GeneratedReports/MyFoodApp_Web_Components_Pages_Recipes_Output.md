# Directory: Components\Pages\Recipes

## File: DisplayModalRecipe.razor

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
                        <FormDisplayRecipeStep Steps="@Recipe.Steps" />

                        <!-- Ingredients -->
                        <FormDisplayIngredient Ingredients="@Recipe.Ingredients" />

                        <!-- Meal Suggestions -->
                        <FormDisplayRecipeMealSuggestion MealSuggestions="@Recipe.MealSuggestions" RecipeId="@Recipe.RecipeId" />
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

## File: EditModalRecipe.razor

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
                            <FormEditRecipeStep Steps="@Recipe.Steps" />

                            <!-- Ingredients -->
                            <FormEditIngredient Ingredients="@Recipe.Ingredients" FoodItemList="FoodItemList" />

                            <!-- Meal Suggestions -->
                            <FormEditRecipeMealSuggestion MealSuggestions="@Recipe.MealSuggestions" MealSuggestionList="MealSuggestionList" />
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

## File: FormDisplayIngredient.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Domain.Enums
@using MyFoodApp.Application.Interfaces
@using System.Globalization
@inject INutritionCalculatorService NutritionCalculatorService

@code {
    [Parameter]
    public required List<IngredientDto> Ingredients { get; set; }

    private Dictionary<IngredientDto, bool> _expandedState = new();
    private NutritionTotals _totals = new();

    protected override void OnParametersSet()
    {
        CalculateTotals();
        InitializeExpandedState();
    }

    private void InitializeExpandedState()
    {
        foreach (var ingredient in Ingredients)
        {
            if (!_expandedState.ContainsKey(ingredient))
            {
                _expandedState[ingredient] = false;
            }
        }
    }

    private void CalculateTotals()
    {
        _totals = new NutritionTotals();
        foreach (var ingredient in Ingredients)
        {
            if (ingredient.FoodItem == null) continue;

            _totals.Calories += NutritionCalculatorService.ConvertToCalories(ingredient);
            _totals.Protein += NutritionCalculatorService.ConvertToProtein(ingredient);
            _totals.Carbohydrates += NutritionCalculatorService.ConvertToCarbohydrates(ingredient);
            _totals.Fat += NutritionCalculatorService.ConvertToFat(ingredient);
        }
    }

    private string GetNutritionProgressStyle(decimal value)
    {
        var percentage = (value / 2000) * 100; // Based on 2000 calorie diet
        return $"width: {Math.Min(percentage, 100)}%;";
    }

    private class NutritionTotals
    {
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fat { get; set; }
    }
}

@if (Ingredients != null)
{
    <div class="card mb-3 shadow-lg">
        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
            <h3 class="mb-0">
                <i class="fas fa-carrot me-2"></i>
                Ingredients (@Ingredients.Count)
            </h3>
            <button class="btn btn-sm btn-light" @onclick="() => CalculateTotals()">
                <i class="fas fa-sync-alt"></i>
            </button>
        </div>

        <div class="card-body">
            @if (Ingredients.Count == 0)
            {
                <div class="text-center py-4 text-muted">
                    <i class="fas fa-inbox fa-3x mb-3"></i>
                    <p class="mb-0">No ingredients added yet</p>
                </div>
            }
            else
            {
                <div class="row g-3 mb-4">
                    <div class="col-md-3">
                        <div class="card bg-light shadow-sm">
                            <div class="card-body">
                                <h6 class="text-uppercase text-muted mb-3">Total Calories</h6>
                                <h2 class="mb-0">@_totals.Calories.ToString("N0")</h2>
                                <div class="progress mt-2" style="height: 5px;">
                                    <div class="progress-bar bg-warning"
                                         style="@GetNutritionProgressStyle(_totals.Calories)"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card bg-light shadow-sm">
                            <div class="card-body">
                                <h6 class="text-uppercase text-muted mb-3">Protein</h6>
                                <h2 class="mb-0">@_totals.Protein.ToString("N1")<small class="text-muted">g</small></h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card bg-light shadow-sm">
                            <div class="card-body">
                                <h6 class="text-uppercase text-muted mb-3">Carbs</h6>
                                <h2 class="mb-0">@_totals.Carbohydrates.ToString("N1")<small class="text-muted">g</small></h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card bg-light shadow-sm">
                            <div class="card-body">
                                <h6 class="text-uppercase text-muted mb-3">Fat</h6>
                                <h2 class="mb-0">@_totals.Fat.ToString("N1")<small class="text-muted">g</small></h2>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container">
                    <div class="row">
                        @foreach (var ingredient in Ingredients)
                        {
                            <div class="col-md-6">
                                <div class="ingredient-card card mb-3 shadow-sm">
                                    <div class="card-header bg-light d-flex justify-content-between align-items-center">
                                        <button class="btn btn-link text-dark text-decoration-none"
                                                @onclick="@(() => _expandedState[ingredient] = !_expandedState[ingredient])">
                                            <i class="fas @(_expandedState[ingredient] ? "fa-chevron-down" : "fa-chevron-right") me-2"></i>
                                            @ingredient.FoodItem?.Name
                                        </button>
                                    </div>

                                    <div class="collapse @(_expandedState[ingredient] ? "show" : "")">
                                        <div class="card-body">
                                            <div class="row g-3 align-items-center">
                                                <div class="col-md-12">
                                                    @if (ingredient.FoodItem != null)
                                                    {
                                                        <p class="mb-1 text-muted text-nowrap">@ingredient.FoodItem.Description</p>
                                                        <div class="nutrition-facts">
                                                            <div class="d-flex justify-content-between">
                                                                <span>Quantity:</span>
                                                                <strong>@ingredient.Quantity.ToString("N2") @ingredient.Unit</strong>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <span>Calories:</span>
                                                                <strong>@NutritionCalculatorService.ConvertToCalories(ingredient).ToString("N0")</strong>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <span>Protein:</span>
                                                                <strong>@NutritionCalculatorService.ConvertToProtein(ingredient).ToString("N2")g</strong>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <span>Carbohydrates:</span>
                                                                <strong>@NutritionCalculatorService.ConvertToCarbohydrates(ingredient).ToString("N2")g</strong>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <span>Fat:</span>
                                                                <strong>@NutritionCalculatorService.ConvertToFat(ingredient).ToString("N2")g</strong>
                                                            </div>
                                                            @if (ingredient.FoodItem.FoodCategory != null)
                                                            {
                                                                <div class="mt-2">
                                                                    <span class="badge bg-info">
                                                                        @ingredient.FoodItem.FoodCategory.Name
                                                                    </span>
                                                                </div>
                                                            }
                                                        </div>
                                                    }
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        }
                    </div>
                </div>
            }
        </div>
    </div>
}

```

## File: FormDisplayRecipeMealSuggestion.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Domain.Enums
@using MyFoodApp.Application.Interfaces
@using System.Globalization
@inject INutritionCalculatorService CalorieCalculatorService

@code {
    [Parameter]
    public required List<RecipeMealSuggestionDto> MealSuggestions { get; set; }

    [Parameter]
    public required int RecipeId { get; set; }

    private Dictionary<RecipeMealSuggestionDto, bool> _expandedState = new();

    protected override void OnParametersSet()
    {
        InitializeExpandedState();
    }

    private void InitializeExpandedState()
    {
        foreach (var suggestion in MealSuggestions)
        {
            if (!_expandedState.ContainsKey(suggestion))
            {
                _expandedState[suggestion] = true;
            }
        }
    }
}

@if (MealSuggestions != null)
{
    <div class="card mb-3 shadow-lg">
        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
            <h3 class="mb-0">
                <i class="fas fa-utensils me-2"></i>
                Meal Suggestions (@MealSuggestions.Count)
            </h3>
        </div>

        <div class="card-body">
            @if (MealSuggestions.Count == 0)
            {
                <div class="text-center py-4 text-muted">
                    <i class="fas fa-inbox fa-3x mb-3"></i>
                    <p class="mb-0">No meal suggestions added yet</p>
                </div>
            }
            else
            {
                <div class="container">
                    <div class="row">
                        @foreach (var suggestion in MealSuggestions)
                        {
                            <div class="col-md-6">
                                <div class="suggestion-card card mb-3 shadow-sm">
                                    <div class="card-header bg-light d-flex justify-content-between align-items-center">
                                        <button class="btn btn-link text-dark text-decoration-none"
                                                @onclick="@(() => _expandedState[suggestion] = !_expandedState[suggestion])">
                                            <i class="fas @(_expandedState[suggestion] ? "fa-chevron-down" : "fa-chevron-right") me-2"></i>
                                            @suggestion.MealSuggestion?.Name
                                        </button>
                                    </div>

                                    <div class="collapse @(_expandedState[suggestion] ? "show" : "")">
                                        <div class="card-body">
                                            <div class="row g-3">
                                                <div class="col-md-12">
                                                    <p><span class="badge bg-primary bg-opacity-10 text-primary me-1">@suggestion.MealSuggestion?.MealType</span> </p>
                                                    <p>
                                                        @if (suggestion.MealSuggestion != null && suggestion.MealSuggestion.RecipeSuggestions != null)
                                                        {
                                                            @foreach (var recipe in suggestion.MealSuggestion.RecipeSuggestions)
                                                            {
                                                                @if (recipe.RecipeId != RecipeId)
                                                                {
                                                                    <span class="badge bg-secondary me-1">@recipe.Recipe?.Title</span>
                                                                }
                                                            }
                                                        }
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        }
                    </div>
                </div>
            }
        </div>
    </div>
}

```

## File: FormDisplayRecipeStep.razor

```C#
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required List<RecipeStepDto> Steps { get; set; }

    private Dictionary<RecipeStepDto, bool> _expandedState = new();

    protected override void OnParametersSet()
    {
        InitializeExpandedState();
    }

    private void InitializeExpandedState()
    {
        foreach (var step in Steps)
        {
            if (!_expandedState.ContainsKey(step))
            {
                _expandedState[step] = true;
            }
        }
    }
}

@if (Steps != null)
{
    <div class="card mb-3 shadow-lg">
        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
            <h3 class="mb-0">
                <i class="fas fa-list-ol me-2"></i>
                Steps (@Steps.Count)
            </h3>
        </div>

        <div class="card-body">
            @if (Steps.Count == 0)
            {
                <div class="text-center py-4 text-muted">
                    <i class="fas fa-inbox fa-3x mb-3"></i>
                    <p class="mb-0">No steps added yet</p>
                </div>
            }
            else
            {
                <ul class="list-unstyled">
                    @foreach (var step in Steps)
                    {
                        <li class="mb-3">
                            <div class="step-card card shadow-sm">
                                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                                    <button class="btn btn-link text-dark text-decoration-none"
                                            @onclick="@(() => _expandedState[step] = !_expandedState[step])">
                                        <i class="fas @(_expandedState[step] ? "fa-chevron-down" : "fa-chevron-right") me-2"></i>
                                        <strong>Step @step.StepNumber</strong>
                                    </button>
                                </div>

                                <div class="collapse @(_expandedState[step] ? "show" : "")">
                                    <div class="card-body">
                                        <div class="form-group">
                                            <label for="step-@step.StepNumber">Instruction</label>
                                            <textarea id="step-@step.StepNumber"
                                                      class="form-control"
                                                      rows="3"
                                                      @bind="step.Instruction" readonly></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    }
                </ul>
            }
        </div>
    </div>
}

```

## File: FormEditIngredient.razor

```C#
@using MyFoodApp.Application.DTOs
@using MyFoodApp.Domain.Enums

@code {
    [Parameter]
    public required List<IngredientDto> Ingredients { get; set; }

    [Parameter]
    public required List<FoodItemDto> FoodItemList { get; set; }

    private void AddIngredient()
    {
        Ingredients.Add(new IngredientDto());
    }

    private void RemoveIngredient(int index)
    {
        Ingredients.RemoveAt(index);
    }
}

@if (Ingredients != null)
{
    <div class="card mb-3">
        <div class="card-header">Ingredients</div>
        <div class="card-body">
            @for (int i = 0; i < Ingredients.Count; i++)
            {
                var index = i;
                <div class="row g-3 mb-3 align-items-end">
                    <div class="col-md-4">
                        <FormEditSelectFoodItem Ingredient="Ingredients[index]" FoodItemList="FoodItemList" />
                    </div>
                    <div class="col-md-3">
                        <div class="form-floating">
                            <InputNumber @bind-Value="Ingredients[index].Quantity" class="form-control" />
                            <label>Quantity</label>
                            <ValidationMessage For="@(() => Ingredients[index].Quantity)" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-floating">
                            <InputSelect @bind-Value="Ingredients[index].Unit" class="form-select">
                                @foreach (var unit in Enum.GetValues(typeof(UnitType)))
                                {
                                    <option value="@unit">@unit</option>
                                }
                            </InputSelect>
                            <label>Unit</label>
                            <ValidationMessage For="@(() => Ingredients[index].Unit)" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-danger" @onclick="() => RemoveIngredient(index)">
                            Remove
                        </button>
                    </div>
                </div>
            }
            <button type="button" class="btn btn-primary" @onclick="AddIngredient">Add Ingredient</button>
        </div>
    </div>
}

```

## File: FormEditRecipeMealSuggestion.razor

```C#
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required List<RecipeMealSuggestionDto> MealSuggestions { get; set; }

    [Parameter]
    public required List<MealSuggestionDto> MealSuggestionList { get; set; }

    private void AddMealSuggestion()
    {
        MealSuggestions.Add(new RecipeMealSuggestionDto());
    }

    private void RemoveMealSuggestion(int index)
    {
        MealSuggestions.RemoveAt(index);
    }
}

@if (MealSuggestions != null)
{
    <div class="card mb-3">
        <div class="card-header">Meal Suggestions</div>
        <div class="card-body">
            @for (int i = 0; i < MealSuggestions.Count; i++)
            {
                var index = i;
                <div class="row g-3 mb-3 align-items-end">
                    <div class="col-md-6">
                        <div class="form-floating">
                            <FormEditSelectMealSuggestion RecipeMealSuggestion="MealSuggestions[i]" MealSuggestionList="MealSuggestionList" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-danger" @onclick="() => RemoveMealSuggestion(index)">
                            Remove
                        </button>
                    </div>
                </div>
            }
            <button type="button" class="btn btn-primary" @onclick="AddMealSuggestion">Add Meal Suggestion</button>
        </div>
    </div>
}

```

## File: FormEditRecipeStep.razor

```C#
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required List<RecipeStepDto> Steps { get; set; }

    private void AddStep()
    {
        Steps.Add(new RecipeStepDto() { Instruction = "", StepNumber = Steps.Count + 1 });
    }

    private void RemoveStep(int index)
    {
        Steps.RemoveAt(index);
    }
}

@if (Steps != null)
{
    <div class="card mb-3">
        <div class="card-header">Steps</div>
        <div class="card-body">
            @for (int i = 0; i < Steps.Count; i++)
            {
                var index = i;
                <div class="row g-3 mb-3 align-items-end">
                    <div class="col-md-2">
                        <div class="form-floating">
                            <InputNumber @bind-Value="Steps[index].StepNumber" class="form-control" />
                            <label>Step Number</label>
                            <ValidationMessage For="@(() => Steps[index].StepNumber)" />
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-floating">
                            <InputTextArea @bind-Value="Steps[index].Instruction" class="form-control" />
                            <label>Instruction</label>
                            <ValidationMessage For="@(() => Steps[index].Instruction)" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-danger" @onclick="() => RemoveStep(index)">
                            Remove
                        </button>
                    </div>
                </div>
            }
            <button type="button" class="btn btn-primary" @onclick="AddStep">Add Step</button>
        </div>
    </div>
}

```

## File: FormEditSelectFoodItem.razor

```C#
@using MyFoodApp.Application.Common
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required IngredientDto Ingredient { get; set; }

    [Parameter]
    public required List<FoodItemDto> FoodItemList { get; set; }
}

<div class="form-floating">
    <InputSelect @bind-Value="Ingredient.FoodItemId" class="form-select">
        @{
            var groupedFoodItems = FoodItemList.GroupBy(fi => fi.FoodCategory?.Name);
        }
        @foreach (var categoryGroup in groupedFoodItems)
        {
            <optgroup label="@categoryGroup.Key">
                @foreach (var foodItem in categoryGroup)
                {
                    <option value="@foodItem.FoodItemId" class="food-item-option"
                            title="@String.Format("Unit Type: {0}\n\tCalories: {2}\n\tProtein: {1}\n\tCarbohydrates: {3}\n\tFat: {4}", @foodItem.Unit, @foodItem.ProteinPerUnit, @foodItem.CaloriesPerUnit, @foodItem.CarbohydratesPerUnit, @foodItem.FatPerUnit)">
                        @foodItem.Name
                    </option>
                }
            </optgroup>
        }
    </InputSelect>

    <label>Food Item</label>
    <ValidationMessage For="@(() => Ingredient.FoodItemId)" />
</div>

```

## File: FormEditSelectMealSuggestion.razor

```C#
@using MyFoodApp.Application.Common
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required RecipeMealSuggestionDto RecipeMealSuggestion { get; set; }

    [Parameter]
    public required List<MealSuggestionDto> MealSuggestionList { get; set; }
}

<div class="form-floating">
    <InputSelect @bind-Value="RecipeMealSuggestion.MealSuggestionId" class="form-select">
        @{
            var groupedMealSuggestions = MealSuggestionList.GroupBy(ms => ms.MealType);
        }
        @foreach (var mealTypeGroup in groupedMealSuggestions)
        {
            <optgroup label="@mealTypeGroup.Key">
                @foreach (var mealSuggestion in mealTypeGroup)
                {
                    <option value="@mealSuggestion.MealSuggestionId">
                        @mealSuggestion.Name
                    </option>
                }
            </optgroup>
        }
    </InputSelect>

    <label>Meal Suggestion Item</label>
    <ValidationMessage For="@(() => RecipeMealSuggestion.MealSuggestionId)" />
</div>

```

## File: IndexDisplayRecipe.razor

```C#
@page "/recipes"
@rendermode InteractiveServer
@attribute [Authorize]

<ListDisplayRecipe />

```

## File: IndexEditRecipe.razor

```C#
@page "/recipes-edit"
@rendermode InteractiveServer
@attribute [Authorize(Roles = "Admin")]

<ListEditRecipe />

```

## File: ListDisplayRecipe.razor

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
    <SearchFormRecipe SearchDto="searchDto" />

    <SearchButton OnSearch="Search" OnReset="ResetSearch" OnAdd="ShowAddModal" />

    <TableDisplayRecipe Recipes="Recipes" OnEdit="ShowEditModal" OnDisplay="ShowDisplayModal" />

    <SearchPagination TItem="RecipeDto" TotalItems="@totalItems" PageSize="@pageSize" CurrentPage="@currentPage" OnPageChanged="OnPageChanged" OnPageSizeChangedCallback="OnPageSizeChanged" />
}

<DisplayModalRecipe @ref="displayModal" />

<EditModalRecipe @ref="editModal" OnRecipeSaved="LoadRecipes" />

@code {
    private List<RecipeDto>? Recipes;
    private bool isLoading = true;
    private EditModalRecipe? editModal;
    private DisplayModalRecipe? displayModal;
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

## File: ListEditRecipe.razor

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
    <SearchFormRecipe SearchDto="searchDto" />

    <SearchButton OnSearch="Search" OnReset="ResetSearch" OnAdd="ShowAddModal" />

    <TableFormRecipe Recipes="Recipes" OnEdit="ShowEditModal" OnDelete="DeleteRecipe" />

    <SearchPagination TItem="RecipeDto" TotalItems="@totalItems" PageSize="@pageSize" CurrentPage="@currentPage" OnPageChanged="OnPageChanged" OnPageSizeChangedCallback="OnPageSizeChanged" />

    <DisplayError Errors="Errors" />
}

<EditModalRecipe @ref="editModal" OnRecipeSaved="LoadRecipes" />

@code {
    private List<RecipeDto>? Recipes;
    private List<Error> Errors { get; set; } = new();
    private bool isLoading = true;
    private EditModalRecipe? editModal;
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

## File: SearchFormRecipe.razor

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

## File: TableDisplayRecipe.razor

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

## File: TableFormRecipe.razor

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

