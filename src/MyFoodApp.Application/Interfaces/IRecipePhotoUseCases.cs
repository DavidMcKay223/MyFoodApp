using Microsoft.AspNetCore.Http;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces
{
    public interface IRecipePhotoUseCases
    {
        Task<Response<RecipePhotoDto>> UploadRecipePhotoAsync(int recipeId, byte[] imageData, string imageContentType, string? caption);
    }
}
