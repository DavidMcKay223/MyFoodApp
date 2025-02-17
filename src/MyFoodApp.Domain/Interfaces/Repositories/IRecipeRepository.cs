﻿using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int recipeId);
        Task<Recipe?> GetRecipeByIdAsync(int recipeId, bool tracking);
        Task<Recipe> AddRecipeAsync(Recipe recipe);
        Task AddRecipeStepRangeAsync(List<RecipeStep> items);
        Task AddIngredientRangeAsync(List<Ingredient> items);
        Task AddRecipeMealSuggestionRangeAsync(List<RecipeMealSuggestion> items);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Recipe recipe);
        IQueryable<Recipe> GetRecipesByIngredientsAsync(IEnumerable<int> foodItemIds);
        Task DeleteIngredientAsync(Ingredient ingredient);
        Task DeleteStepAsync(RecipeStep step);
        Task DeleteMealSuggestionAsync(RecipeMealSuggestion suggestion);
    }
}
