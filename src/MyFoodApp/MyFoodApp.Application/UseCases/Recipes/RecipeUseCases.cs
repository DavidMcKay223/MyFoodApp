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

                // Handle related entities
                foreach (var ingredientDto in recipeDto.Ingredients)
                {
                    var ingredient = _mapper.Map<Ingredient>(ingredientDto);
                    recipe.Ingredients.Add(ingredient);
                }

                foreach (var stepDto in recipeDto.Steps)
                {
                    var step = _mapper.Map<RecipeStep>(stepDto);
                    recipe.Steps.Add(step);
                }

                foreach (var mealSuggestionDto in recipeDto.MealSuggestions)
                {
                    var mealSuggestion = _mapper.Map<RecipeMealSuggestion>(mealSuggestionDto);
                    recipe.MealSuggestions.Add(mealSuggestion);
                }

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

                // Remove Ingredients that are not in the DTO
                var ingredientsToRemove = recipe.Ingredients.Where(i => !recipeDto.Ingredients.Any(dto => dto.IngredientId == i.IngredientId)).ToList();
                foreach (var ingredient in ingredientsToRemove)
                {
                    recipe.Ingredients.Remove(ingredient);
                }

                // Add or update Ingredients
                foreach (var ingredientDto in recipeDto.Ingredients)
                {
                    var existingIngredient = recipe.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientDto.IngredientId);
                    if (existingIngredient != null)
                    {
                        _mapper.Map(ingredientDto, existingIngredient);
                    }
                    else
                    {
                        var ingredient = _mapper.Map<Ingredient>(ingredientDto);
                        recipe.Ingredients.Add(ingredient);
                    }
                }

                // Remove RecipeSteps that are not in the DTO
                var stepsToRemove = recipe.Steps.Where(s => !recipeDto.Steps.Any(dto => dto.StepId == s.StepId)).ToList();
                foreach (var step in stepsToRemove)
                {
                    recipe.Steps.Remove(step);
                }

                // Add or update RecipeSteps
                foreach (var stepDto in recipeDto.Steps)
                {
                    var existingStep = recipe.Steps.FirstOrDefault(s => s.StepId == stepDto.StepId);
                    if (existingStep != null)
                    {
                        _mapper.Map(stepDto, existingStep);
                    }
                    else
                    {
                        var step = _mapper.Map<RecipeStep>(stepDto);
                        recipe.Steps.Add(step);
                    }
                }

                // Remove MealSuggestions that are not in the DTO
                var mealSuggestionsToRemove = recipe.MealSuggestions.Where(ms => !recipeDto.MealSuggestions.Any(dto => dto.MealSuggestionId == ms.MealSuggestionId)).ToList();
                foreach (var mealSuggestion in mealSuggestionsToRemove)
                {
                    recipe.MealSuggestions.Remove(mealSuggestion);
                }

                // Add or update MealSuggestions
                foreach (var mealSuggestionDto in recipeDto.MealSuggestions)
                {
                    var existingMealSuggestion = recipe.MealSuggestions.FirstOrDefault(ms => ms.MealSuggestionId == mealSuggestionDto.MealSuggestionId);
                    if (existingMealSuggestion != null)
                    {
                        _mapper.Map(mealSuggestionDto, existingMealSuggestion);
                    }
                    else
                    {
                        var mealSuggestion = _mapper.Map<RecipeMealSuggestion>(mealSuggestionDto);
                        recipe.MealSuggestions.Add(mealSuggestion);
                    }
                }

                // Map updated values to recipe
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
