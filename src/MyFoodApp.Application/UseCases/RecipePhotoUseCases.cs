using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.UseCases
{
    public class RecipePhotoUseCases : IRecipePhotoUseCases
    {
        private readonly IRecipePhotoRepository _recipePhotoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipePhotoUseCases> _logger;

        public RecipePhotoUseCases(
            IRecipePhotoRepository recipePhotoRepository,
            IMapper mapper,
            ILogger<RecipePhotoUseCases> logger)
        {
            _recipePhotoRepository = recipePhotoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<RecipePhotoDto>> UploadRecipePhotoAsync(int recipeId, byte[] imageData, string imageContentType, string? caption)
        {
            var response = new Response<RecipePhotoDto>();

            try
            {
                var recipePhoto = new RecipePhoto
                {
                    RecipeId = recipeId,
                    ImageData = imageData,
                    ImageContentType = imageContentType,
                    Caption = caption,
                    UploadDate = DateTime.UtcNow
                };

                await _recipePhotoRepository.AddAsync(recipePhoto);

                response.Item = _mapper.Map<RecipePhotoDto>(recipePhoto);
                response.TotalItems = 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading photo for recipe {recipeId}.");
                response.ErrorList.Add(new Error { Code = "UploadError", Message = "Error uploading image." });
            }

            return response;
        }
    }
}
