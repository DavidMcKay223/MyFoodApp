using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces.Meals
{
    public interface IMealSuggestionUseCases
    {
        Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId);
        Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync();
    }
}
