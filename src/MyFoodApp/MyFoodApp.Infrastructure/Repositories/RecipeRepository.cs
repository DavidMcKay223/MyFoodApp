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
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f!.FoodCategory)
                .Include(r => r.MealSuggestions)
                    .ThenInclude(ms => ms.MealSuggestion)
                .AsNoTracking()
                .AsQueryable();

            return recipes;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            var query = _context.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f!.FoodCategory)
                .Include(r => r.MealSuggestions)
                    .ThenInclude(ms => ms.MealSuggestion)
                        .ThenInclude(ms => ms.RecipeSuggestions)
                            .ThenInclude(ms => ms.Recipe);

            var recipe = await query.FirstOrDefaultAsync(r => r.RecipeId == id);

            return recipe;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id, bool tracking)
        {
            var query = _context.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .Include(r => r.MealSuggestions);

            return tracking
                ? await query.FirstOrDefaultAsync(r => r.RecipeId == id)
                : await query.AsNoTracking().FirstOrDefaultAsync(r => r.RecipeId == id);
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
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f!.FoodCategory)
                .Include(r => r.MealSuggestions)
                    .ThenInclude(ms => ms.MealSuggestion)
                .Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.FoodItemId)))
                .AsQueryable();

            return recipes;
        }

        public async Task AddRecipeStepRangeAsync(List<RecipeStep> items)
        {
            await _context.RecipeSteps.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task AddIngredientRangeAsync(List<Ingredient> items)
        {
            await _context.Ingredients.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task AddRecipeMealSuggestionRangeAsync(List<RecipeMealSuggestion> items)
        {
            await _context.RecipeMealSuggestions.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStepAsync(RecipeStep step)
        {
            _context.RecipeSteps.Remove(step);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealSuggestionAsync(RecipeMealSuggestion suggestion)
        {
            _context.RecipeMealSuggestions.Remove(suggestion);
            await _context.SaveChangesAsync();
        }
    }
}
