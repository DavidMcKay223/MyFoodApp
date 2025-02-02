using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IFoodItemRepository
    {
        IQueryable<FoodItem> GetAllFoodItemsAsync();
        Task<FoodItem?> GetFoodItemByIdAsync(int foodItemId);
    }
}
