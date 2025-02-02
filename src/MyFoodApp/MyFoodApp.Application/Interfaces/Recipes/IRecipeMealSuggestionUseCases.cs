using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces.Recipes
{
    public interface IRecipeMealSuggestionUseCases
    {
        Task<Response<RecipeMealSuggestionDto>> GetRecipeMealSuggestionByIdAsync(int foodItemId);
        Task<Response<RecipeMealSuggestionDto>> GetRecipeMealSuggestionListAsync();
    }
}
