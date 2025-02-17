﻿using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IFoodItemUseCases
    {
        Task<Response<FoodItemDto>> GetFoodItemByIdAsync(int foodItemId);
        Task<Response<FoodItemDto>> GetFoodItemListAsync();
    }
}
