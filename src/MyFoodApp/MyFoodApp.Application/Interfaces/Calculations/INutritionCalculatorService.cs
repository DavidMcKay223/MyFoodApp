using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces.Calculations
{
    public interface INutritionCalculatorService
    {
        string CalculateCalories(RecipeDto recipe);
        decimal ConvertToCalories(IngredientDto ingredient);
        string CalculateProtein(RecipeDto recipe);
        decimal ConvertToProtein(IngredientDto ingredient);
        string CalculateCarbohydrates(RecipeDto recipe);
        decimal ConvertToCarbohydrates(IngredientDto ingredient);
        string CalculateFat(RecipeDto recipe);
        decimal ConvertToFat(IngredientDto ingredient);
    }
}
