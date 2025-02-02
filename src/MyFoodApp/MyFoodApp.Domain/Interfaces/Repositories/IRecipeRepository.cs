using MyFoodApp.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        Task<IQueryable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int recipeId);
        Task<Recipe> AddAsync(Recipe recipe);
        Task<Recipe> UpdateAsync(Recipe recipe);
        Task DeleteAsync(Recipe recipe);
        Task<IQueryable<Recipe>> GetRecipesByIngredientsAsync(IEnumerable<int> ingredientIds);
    }
}
