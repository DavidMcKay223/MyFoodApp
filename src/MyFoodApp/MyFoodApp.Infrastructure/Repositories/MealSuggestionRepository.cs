using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.Infrastructure.Repositories
{
    public class MealSuggestionRepository : IMealSuggestionRepository
    {
        private readonly AppDbContext _context;

        public MealSuggestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<MealSuggestion> GetAllMealSuggestionsAsync()
        {
            var recipes = _context.MealSuggestions
                .AsNoTracking()
                .AsQueryable();

            return recipes;
        }

        public async Task<MealSuggestion?> GetMealSuggestionByIdAsync(int MealSuggestionId, bool tracking = false)
        {
            return tracking
                ? await _context.MealSuggestions.FirstOrDefaultAsync(r => r.MealSuggestionId == MealSuggestionId)
                : await _context.MealSuggestions.AsNoTracking().FirstOrDefaultAsync(r => r.MealSuggestionId == MealSuggestionId);
        }
    }
}
