using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces.Meals
{
    public interface IMealSuggestionUseCases
    {
        Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId);
        Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync();
    }
}
