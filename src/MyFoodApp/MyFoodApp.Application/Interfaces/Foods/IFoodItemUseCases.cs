using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces.Foods
{
    public interface IFoodItemUseCases
    {
        Task<Response<FoodItemDto>> GetFoodItemByIdAsync(int foodItemId);
        Task<Response<FoodItemDto>> GetFoodItemListAsync();
    }
}
