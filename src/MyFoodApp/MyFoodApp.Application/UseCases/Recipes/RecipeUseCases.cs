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

                // Handle ingredients with existing food items
                foreach (var ingredientDto in recipeDto.Ingredients)
                {
                    var ingredient = new Ingredient
                    {
                        Quantity = ingredientDto.Quantity,
                        Unit = ingredientDto.Unit,
                        FoodItemId = ingredientDto.FoodItemId // Set FK directly
                    };

                    recipe.Ingredients.Add(ingredient);
                }

                // Keep existing mapping for steps and meal suggestions
                foreach (var stepDto in recipeDto.Steps)
                {
                    var step = _mapper.Map<RecipeStep>(stepDto);
                    recipe.Steps.Add(step);
                }

                foreach (var mealSuggestionDto in recipeDto.MealSuggestions)
                {
                    var mealSuggestion = new RecipeMealSuggestion
                    {
                        MealSuggestionId = mealSuggestionDto.MealSuggestionId
                    };
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
                var recipe = await _recipeRepository.GetRecipeByIdAsync(recipeId, true);
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
                var query = _recipeRepository.GetAllRecipesAsync()
                    .Include(r => r.Steps)
                    .Include(r => r.Ingredients)
                        .ThenInclude(i => i.FoodItem)
                            .ThenInclude(f => f.FoodCategory)
                    .Include(r => r.MealSuggestions)
                        .ThenInclude(ms => ms.MealSuggestion)
                    .AsQueryable();

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
                // Get existing recipe with tracking
                var recipe = await _recipeRepository.GetRecipeByIdAsync(recipeId, true);
                if (recipe == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Recipe not found." });
                    return response;
                }

                // Update main recipe properties
                _mapper.Map(recipeDto, recipe);

                #region Ingredients Update
                // Process ingredients
                var existingIngredientIds = recipe.Ingredients.Select(i => i.IngredientId).ToList();

                // Update existing ingredients and add new ones
                foreach (var ingredientDto in recipeDto.Ingredients)
                {
                    if (ingredientDto.IngredientId > 0 && existingIngredientIds.Contains(ingredientDto.IngredientId))
                    {
                        // Update existing ingredient
                        var existingIngredient = recipe.Ingredients.First(i => i.IngredientId == ingredientDto.IngredientId);
                        _mapper.Map(ingredientDto, existingIngredient);

                        // Set FoodItem by ID only
                        existingIngredient.FoodItemId = ingredientDto.FoodItemId;
                    }
                    else
                    {
                        // Add new ingredient
                        var newIngredient = _mapper.Map<Ingredient>(ingredientDto);
                        newIngredient.FoodItemId = ingredientDto.FoodItemId; // Set direct relationship
                        recipe.Ingredients.Add(newIngredient);
                    }
                }

                // Remove deleted ingredients
                var currentIngredientIds = recipeDto.Ingredients.Select(i => i.IngredientId).ToList();
                var ingredientsToRemove = recipe.Ingredients
                    .Where(i => !currentIngredientIds.Contains(i.IngredientId))
                    .ToList();

                foreach (var ingredient in ingredientsToRemove)
                {
                    recipe.Ingredients.Remove(ingredient);
                }
                #endregion

                #region Steps Update
                // Process steps
                var existingStepIds = recipe.Steps.Select(s => s.StepId).ToList();

                foreach (var stepDto in recipeDto.Steps)
                {
                    if (stepDto.StepId > 0 && existingStepIds.Contains(stepDto.StepId))
                    {
                        var existingStep = recipe.Steps.First(s => s.StepId == stepDto.StepId);
                        _mapper.Map(stepDto, existingStep);
                    }
                    else
                    {
                        recipe.Steps.Add(_mapper.Map<RecipeStep>(stepDto));
                    }
                }

                // Remove deleted steps
                var currentStepIds = recipeDto.Steps.Select(s => s.StepId).ToList();
                var stepsToRemove = recipe.Steps
                    .Where(s => !currentStepIds.Contains(s.StepId))
                    .ToList();

                foreach (var step in stepsToRemove)
                {
                    recipe.Steps.Remove(step);
                }
                #endregion

                #region Meal Suggestions Update
                // Process meal suggestions
                var existingMealSuggestionIds = recipe.MealSuggestions
                    .Select(ms => ms.MealSuggestionId)
                    .ToList();

                foreach (var mealSuggestionDto in recipeDto.MealSuggestions)
                {
                    if (existingMealSuggestionIds.Contains(mealSuggestionDto.MealSuggestionId))
                    {
                        // Existing relationship - no need to update
                        continue;
                    }

                    // Add new relationship
                    recipe.MealSuggestions.Add(new RecipeMealSuggestion
                    {
                        MealSuggestionId = mealSuggestionDto.MealSuggestionId
                    });
                }

                // Remove deleted meal suggestions
                var currentMealSuggestionIds = recipeDto.MealSuggestions
                    .Select(ms => ms.MealSuggestionId)
                    .ToList();

                var mealSuggestionsToRemove = recipe.MealSuggestions
                    .Where(ms => !currentMealSuggestionIds.Contains(ms.MealSuggestionId))
                    .ToList();

                foreach (var mealSuggestion in mealSuggestionsToRemove)
                {
                    recipe.MealSuggestions.Remove(mealSuggestion);
                }
                #endregion

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
