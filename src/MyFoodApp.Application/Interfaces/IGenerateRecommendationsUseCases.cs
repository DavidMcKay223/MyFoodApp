using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IGenerateRecommendationsUseCases
    {
        Task<Response<MealSuggestionTagDto>> GetMealSuggestionWithRecipeListAsync();
    }
}
