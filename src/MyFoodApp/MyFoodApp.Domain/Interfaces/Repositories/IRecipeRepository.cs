using MyFoodApp.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int recipeId);
        Task<Recipe> AddRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Recipe recipe);
        IQueryable<Recipe> GetRecipesByIngredientsAsync(IEnumerable<int> ingredientIds);
    }
}
