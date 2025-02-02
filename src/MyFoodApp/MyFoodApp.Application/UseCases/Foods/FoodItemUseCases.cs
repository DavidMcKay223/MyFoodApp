using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces.Foods;
using MyFoodApp.Application.UseCases.Recipes;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.UseCases.Foods
{
    public class FoodItemUseCases : IFoodItemUseCases
    {
        private readonly IFoodItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeUseCases> _logger;

        public FoodItemUseCases(IFoodItemRepository repository, IMapper mapper, ILogger<RecipeUseCases> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<FoodItemDto>> GetFoodItemByIdAsync(int foodItemId)
        {
            var response = new Response<FoodItemDto>();

            try
            {
                var foodItem = await _repository.GetByIdAsync(foodItemId);
                if (foodItem == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Food Item not found." });
                    return response;
                }

                response.Item = _mapper.Map<FoodItemDto>(foodItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recipe by id.");
                response.ErrorList.Add(new Error { Code = "GetByIdError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<FoodItemDto>> GetFoodItemListAsync()
        {
            var response = new Response<FoodItemDto>();

            try
            {
                var query = await _repository.GetAllAsync().ToListAsync();

                response.List = _mapper.Map<List<FoodItemDto>>(query);
                response.TotalItems = response.List.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while looking up recipes.");
                response.ErrorList.Add(new Error { Code = "LookupError", Message = ex.Message });
            }

            return response;
        }
    }
}
