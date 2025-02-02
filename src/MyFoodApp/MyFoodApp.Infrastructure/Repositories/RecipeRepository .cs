using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Recipe> GetAllRecipesAsync()
        {
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.MealSuggestions)
                .AsQueryable();

            return recipes;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int recipeId)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.MealSuggestions)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        }

        public async Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task DeleteRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Recipe> GetRecipesByIngredientsAsync(IEnumerable<int> ingredientIds)
        {
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.FoodItemId)))
                .AsQueryable();

            return recipes;
        }
    }
}
