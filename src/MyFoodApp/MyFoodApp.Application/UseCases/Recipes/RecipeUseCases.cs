using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces.Recipes;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MyFoodApp.Application.UseCases.Recipes
{
    public class RecipeUseCases : IRecipeUseCases
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeUseCases> _logger;

        public RecipeUseCases(IRecipeRepository recipeRepository, IMapper mapper, ILogger<RecipeUseCases> logger)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<RecipeDto>> CreateRecipeAsync(RecipeDto recipeDto)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipe = _mapper.Map<Recipe>(recipeDto);
                await _recipeRepository.AddRecipeAsync(recipe);
                response.Item = _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating recipe.");
                response.ErrorList.Add(new Error { Code = "CreateError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<RecipeDto>> DeleteRecipeAsync(int recipeId)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipe = await _recipeRepository.GetRecipeByIdAsync(recipeId);
                if (recipe == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Recipe not found." });
                    return response;
                }

                await _recipeRepository.DeleteRecipeAsync(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting recipe.");
                response.ErrorList.Add(new Error { Code = "DeleteError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<RecipeDto>> GetRecipeByIdAsync(int recipeId)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipe = await _recipeRepository.GetRecipeByIdAsync(recipeId);
                if (recipe == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Recipe not found." });
                    return response;
                }

                response.Item = _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recipe by id.");
                response.ErrorList.Add(new Error { Code = "GetByIdError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<RecipeDto>> LookupRecipesAsync(RecipeSearchDto searchDto)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var query = _recipeRepository.GetAllRecipesAsync();

                // Apply filters based on searchDto properties
                if (!string.IsNullOrEmpty(searchDto.Title))
                {
                    query = query.Where(r => r.Title.Contains(searchDto.Title));
                }
                if (!string.IsNullOrEmpty(searchDto.Description))
                {
                    query = query.Where(r => r.Description.Contains(searchDto.Description));
                }
                if (searchDto.PrepTimeMin.HasValue)
                {
                    query = query.Where(r => r.PrepTimeMinutes >= searchDto.PrepTimeMin.Value);
                }
                if (searchDto.PrepTimeMax.HasValue)
                {
                    query = query.Where(r => r.PrepTimeMinutes <= searchDto.PrepTimeMax.Value);
                }
                if (searchDto.CookTimeMin.HasValue)
                {
                    query = query.Where(r => r.CookTimeMinutes >= searchDto.CookTimeMin.Value);
                }
                if (searchDto.CookTimeMax.HasValue)
                {
                    query = query.Where(r => r.CookTimeMinutes <= searchDto.CookTimeMax.Value);
                }
                if (searchDto.ServingsMin.HasValue)
                {
                    query = query.Where(r => r.Servings >= searchDto.ServingsMin.Value);
                }
                if (searchDto.ServingsMax.HasValue)
                {
                    query = query.Where(r => r.Servings <= searchDto.ServingsMax.Value);
                }

                var recipes = await query.ToListAsync();
                response.List = _mapper.Map<List<RecipeDto>>(recipes);
                response.TotalItems = response.List.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while looking up recipes.");
                response.ErrorList.Add(new Error { Code = "LookupError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<RecipeDto>> SuggestRecipesBasedOnIngredientsAsync(IEnumerable<int> ingredientIds)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipes = _recipeRepository.GetRecipesByIngredientsAsync(ingredientIds);
                response.List = _mapper.Map<List<RecipeDto>>(recipes);
                response.TotalItems = response.List.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while suggesting recipes.");
                response.ErrorList.Add(new Error { Code = "SuggestionError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<RecipeDto>> UpdateRecipeAsync(int recipeId, RecipeDto recipeDto)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipe = await _recipeRepository.GetRecipeByIdAsync(recipeId);
                if (recipe == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Recipe not found." });
                    return response;
                }

                _mapper.Map(recipeDto, recipe);
                await _recipeRepository.UpdateRecipeAsync(recipe);
                response.Item = _mapper.Map<RecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating recipe.");
                response.ErrorList.Add(new Error { Code = "UpdateError", Message = ex.Message });
            }

            return response;
        }
    }
}
