using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces.Recipes
{
    public interface IRecipeMealSuggestionUseCases
    {
        Task<Response<RecipeMealSuggestionDto>> GetRecipeMealSuggestionByIdAsync(int foodItemId);
        Task<Response<RecipeMealSuggestionDto>> GetRecipeMealSuggestionListAsync();
    }
}
