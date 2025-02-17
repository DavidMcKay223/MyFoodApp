using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IMealSuggestionUseCases
    {
        Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId);
        Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync();
    }
}
