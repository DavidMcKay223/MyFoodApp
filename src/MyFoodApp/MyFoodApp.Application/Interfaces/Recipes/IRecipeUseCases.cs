using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces.Recipes
{
    public interface IRecipeUseCases
    {
        Task<Response<RecipeDto>> LookupRecipesAsync(RecipeSearchDto searchDto);
        Task<Response<RecipeDto>> SuggestRecipesBasedOnIngredientsAsync(IEnumerable<int> ingredientIds);
        Task<Response<RecipeDto>> GetRecipeByIdAsync(int recipeId);
        Task<Response<RecipeDto>> CreateRecipeAsync(RecipeDto recipeDto);
        Task<Response<RecipeDto>> UpdateRecipeAsync(int recipeId, RecipeDto recipeDto);
        Task<Response<RecipeDto>> DeleteRecipeAsync(int recipeId);
    }
}
