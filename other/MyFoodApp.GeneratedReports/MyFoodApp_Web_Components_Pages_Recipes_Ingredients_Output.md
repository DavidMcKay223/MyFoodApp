# Directory: Components\Pages\Recipes\Ingredients

## File: Display.razor

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

## File: Edit.razor

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
                        <SelectFoodItem Ingredient="Ingredients[index]" FoodItemList="FoodItemList" />
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

## File: SelectFoodItem.razor

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

