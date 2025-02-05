using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces.Calculations
{
    public interface ICalorieCalculatorService
    {
        string CalculateCalories(RecipeDto recipe);
        decimal ConvertToCalories(IngredientDto ingredient);
    }
}
