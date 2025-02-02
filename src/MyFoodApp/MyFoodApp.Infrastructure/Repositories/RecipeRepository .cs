using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IQueryable<Recipe>> GetAllAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.MealSuggestions)
                .ToListAsync();

            return recipes.AsQueryable();
        }

        public async Task<Recipe?> GetByIdAsync(int recipeId)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.MealSuggestions)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        }

        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Recipe>> GetRecipesByIngredientsAsync(IEnumerable<int> ingredientIds)
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.FoodItemId)))
                .ToListAsync();

            return recipes.AsQueryable();
        }
    }
}
