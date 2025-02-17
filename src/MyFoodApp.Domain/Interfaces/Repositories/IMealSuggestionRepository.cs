using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IMealSuggestionRepository
    {
        IQueryable<MealSuggestion> GetAllMealSuggestionsAsync();
        Task<MealSuggestion?> GetMealSuggestionByIdAsync(int foodItemId, bool tracking = false);
    }
}
