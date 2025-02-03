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
                // Map DTO to Recipe entity including child collections
                var recipeEntity = new Recipe
                {
                    Title = recipeDto.Title,
                    Description = recipeDto.Description,
                    CookTimeMinutes = recipeDto.CookTimeMinutes,
                    PrepTimeMinutes = recipeDto.PrepTimeMinutes,
                    Servings = recipeDto.Servings,
                    Ingredients = recipeDto.Ingredients?.Select(dto => new Ingredient
                    {
                        FoodItemId = dto.FoodItemId,
                        Quantity = dto.Quantity,
                        Unit = dto.Unit,
                    }).ToList() ?? new List<Ingredient>(),
                    Steps = recipeDto.Steps?.Select(dto => new RecipeStep
                    {
                        Instruction = dto.Instruction,
                        StepNumber = dto.StepNumber,
                    }).ToList() ?? new List<RecipeStep>(),
                    MealSuggestions = recipeDto.MealSuggestions?.Select(dto => new RecipeMealSuggestion
                    {
                        MealSuggestionId = dto.MealSuggestionId,
                    }).ToList() ?? new List<RecipeMealSuggestion>()
                };

                // Add the recipe along with its children in one call
                var createdRecipe = await _recipeRepository.AddRecipeAsync(recipeEntity);
                await _recipeRepository.SaveChangesAsync(); // Ensure changes are saved

                response.Item = _mapper.Map<RecipeDto>(createdRecipe);
                response.TotalItems = 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating recipe.");
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

        public async Task<Response<RecipeDto>> SuggestRecipesBasedOnIngredientsAsync(IEnumerable<int> foodItemIds)
        {
            var response = new Response<RecipeDto>();

            try
            {
                var recipes = await _recipeRepository.GetRecipesByIngredientsAsync(foodItemIds).ToListAsync();
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
                // 1. Get existing recipe with all related entities
                var existingRecipe = await _recipeRepository.GetRecipeByIdAsync(recipeId, true);
                if (existingRecipe == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Recipe not found" });
                    return response;
                }

                // 2. Update root recipe properties
                _mapper.Map(recipeDto, existingRecipe);

                // 3. Handle Ingredients
                await UpdateIngredientsAsync(existingRecipe, recipeDto.Ingredients);

                // 4. Handle Steps
                await UpdateStepsAsync(existingRecipe, recipeDto.Steps);

                // 5. Handle Meal Suggestions
                await UpdateMealSuggestionsAsync(existingRecipe, recipeDto.MealSuggestions);

                // 6. Save changes
                await _recipeRepository.UpdateRecipeAsync(existingRecipe);
                await _recipeRepository.SaveChangesAsync();

                // 7. Return updated recipe
                var updatedRecipeDto = _mapper.Map<RecipeDto>(existingRecipe);
                response.Item = updatedRecipeDto;
                response.TotalItems = 1;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error updating recipe");
                response.ErrorList.Add(new Error { Code = "DatabaseError", Message = "Error saving recipe changes" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating recipe");
                response.ErrorList.Add(new Error { Code = "UpdateError", Message = ex.Message });
            }

            return response;
        }

        private async Task UpdateIngredientsAsync(Recipe existingRecipe, List<IngredientDto> updatedIngredients)
        {
            // Handle null cases
            updatedIngredients ??= new List<IngredientDto>();

            // Create dictionary for existing ingredients
            var existingIngredients = existingRecipe.Ingredients.ToDictionary(i => i.IngredientId);

            // Process updates and additions
            foreach (var updatedIngredient in updatedIngredients)
            {
                if (existingIngredients.TryGetValue(updatedIngredient.IngredientId, out var existingIngredient))
                {
                    // Update existing ingredient
                    _mapper.Map(updatedIngredient, existingIngredient);
                    existingIngredient.FoodItemId = updatedIngredient.FoodItemId;
                }
                else
                {
                    // Add new ingredient
                    var newIngredient = _mapper.Map<Ingredient>(updatedIngredient);
                    newIngredient.RecipeId = existingRecipe.RecipeId;
                    existingRecipe.Ingredients.Add(newIngredient);
                }
            }

            // Process deletions
            var updatedIds = updatedIngredients.Select(i => i.IngredientId).ToHashSet();
            var ingredientsToRemove = existingRecipe.Ingredients
                .Where(i => !updatedIds.Contains(i.IngredientId))
                .ToList();

            foreach (var ingredient in ingredientsToRemove)
            {
                existingRecipe.Ingredients.Remove(ingredient);
                await _recipeRepository.DeleteIngredientAsync(ingredient);
            }
        }

        private async Task UpdateStepsAsync(Recipe existingRecipe, List<RecipeStepDto> updatedSteps)
        {
            updatedSteps ??= new List<RecipeStepDto>();

            var existingSteps = existingRecipe.Steps.ToDictionary(s => s.StepId);

            foreach (var updatedStep in updatedSteps)
            {
                if (existingSteps.TryGetValue(updatedStep.StepId, out var existingStep))
                {
                    _mapper.Map(updatedStep, existingStep);
                }
                else
                {
                    var newStep = _mapper.Map<RecipeStep>(updatedStep);
                    newStep.RecipeId = existingRecipe.RecipeId;
                    existingRecipe.Steps.Add(newStep);
                }
            }

            var updatedStepIds = updatedSteps.Select(s => s.StepId).ToHashSet();
            var stepsToRemove = existingRecipe.Steps
                .Where(s => !updatedStepIds.Contains(s.StepId))
                .ToList();

            foreach (var step in stepsToRemove)
            {
                existingRecipe.Steps.Remove(step);
                await _recipeRepository.DeleteStepAsync(step);
            }
        }

        private async Task UpdateMealSuggestionsAsync(Recipe existingRecipe, List<RecipeMealSuggestionDto> updatedSuggestions)
        {
            updatedSuggestions ??= new List<RecipeMealSuggestionDto>();

            var existingSuggestions = existingRecipe.MealSuggestions
                .ToDictionary(ms => ms.MealSuggestionId);

            var updatedSuggestionIds = updatedSuggestions
                .Select(ms => ms.MealSuggestionId)
                .ToHashSet();

            // Add new suggestions
            foreach (var suggestionId in updatedSuggestionIds)
            {
                if (!existingSuggestions.ContainsKey(suggestionId))
                {
                    existingRecipe.MealSuggestions.Add(new RecipeMealSuggestion
                    {
                        RecipeId = existingRecipe.RecipeId,
                        MealSuggestionId = suggestionId
                    });
                }
            }

            // Remove deleted suggestions
            var suggestionsToRemove = existingRecipe.MealSuggestions
                .Where(ms => !updatedSuggestionIds.Contains(ms.MealSuggestionId))
                .ToList();

            foreach (var suggestion in suggestionsToRemove)
            {
                existingRecipe.MealSuggestions.Remove(suggestion);
                await _recipeRepository.DeleteMealSuggestionAsync(suggestion);
            }
        }
    }
}
