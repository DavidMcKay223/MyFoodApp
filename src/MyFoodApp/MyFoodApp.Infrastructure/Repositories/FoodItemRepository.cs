using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.Infrastructure.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly AppDbContext _context;

        public FoodItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<FoodItem> GetAllFoodItemsAsync()
        {
            var recipes = _context.FoodItems
                .Include(x => x.FoodCategory)
                .OrderBy(x => x.FoodCategory!.Name)
                    .ThenBy(x => x.Name)
                .AsNoTracking()
                .AsQueryable();

            return recipes;
        }

        public async Task<FoodItem?> GetFoodItemByIdAsync(int foodItemId, bool tracking = false)
        {
            return tracking
                ? await _context.FoodItems.FirstOrDefaultAsync(r => r.FoodItemId == foodItemId)
                : await _context.FoodItems.AsNoTracking().FirstOrDefaultAsync(r => r.FoodItemId == foodItemId);
        }
    }
}
