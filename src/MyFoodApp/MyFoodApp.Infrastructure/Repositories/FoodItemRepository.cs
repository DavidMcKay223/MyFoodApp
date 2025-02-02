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
                .AsNoTracking()
                .AsQueryable();

            return recipes;
        }

        public async Task<FoodItem?> GetFoodItemByIdAsync(int foodItemId)
        {
            return await _context.FoodItems
                .FirstOrDefaultAsync(r => r.FoodItemId == foodItemId);
        }
    }
}
