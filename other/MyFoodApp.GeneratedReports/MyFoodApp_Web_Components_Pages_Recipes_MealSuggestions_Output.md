# Directory: Components\Pages\Recipes\MealSuggestions

## File: Display.razor

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

## File: Edit.razor

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
                            <SelectMealSuggestion RecipeMealSuggestion="MealSuggestions[i]" MealSuggestionList="MealSuggestionList" />
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

## File: SelectMealSuggestion.razor

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

