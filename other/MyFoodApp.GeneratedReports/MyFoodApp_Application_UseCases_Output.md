# Directory: UseCases

## File: FoodItemUseCases.cs

```C#
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;

namespace MyFoodApp.Application.UseCases
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
                var foodItem = await _repository.GetFoodItemByIdAsync(foodItemId);
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
                var query = await _repository.GetAllFoodItemsAsync().ToListAsync();

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

```

## File: GenerateRecommendationsUseCases.cs

```C#
using MyFoodApp.Application.Interfaces;

namespace MyFoodApp.Application.UseCases
{
    public class GenerateRecommendationsUseCases : IGenerateRecommendationsUseCases
    {

    }
}

```

## File: MealSuggestionUseCases.cs

```C#
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;

namespace MyFoodApp.Application.UseCases
{
    public class MealSuggestionUseCases : IMealSuggestionUseCases
    {
        private readonly IMealSuggestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MealSuggestionUseCases> _logger;

        public MealSuggestionUseCases(IMealSuggestionRepository repository, IMapper mapper, ILogger<MealSuggestionUseCases> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId)
        {
            var response = new Response<MealSuggestionDto>();

            try
            {
                var mealSuggestion = await _repository.GetMealSuggestionByIdAsync(mealSuggestionId);
                if (mealSuggestion == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Meal Suggestion not found." });
                    return response;
                }

                response.Item = _mapper.Map<MealSuggestionDto>(mealSuggestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recipe by id.");
                response.ErrorList.Add(new Error { Code = "GetByIdError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync()
        {
            var response = new Response<MealSuggestionDto>();

            try
            {
                var query = await _repository.GetAllMealSuggestionsAsync().ToListAsync();

                response.List = _mapper.Map<List<MealSuggestionDto>>(query);
                response.TotalItems = response.List.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while looking up meal suggestion.");
                response.ErrorList.Add(new Error { Code = "LookupError", Message = ex.Message });
            }

            return response;
        }
    }
}

```

## File: NutritionCalculatorService.cs

```C#
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Enums;
using MyFoodApp.Application.Interfaces;

namespace MyFoodApp.Application.UseCases
{
    public class NutritionCalculatorService : INutritionCalculatorService
    {
        public string CalculateCalories(RecipeDto recipe)
        {
            try
            {
                var totalCalories = recipe.Ingredients
                    .Sum(i => ConvertToCalories(i));

                var perServing = totalCalories / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateProtein(RecipeDto recipe)
        {
            try
            {
                var totalProtein = recipe.Ingredients
                    .Sum(i => ConvertToProtein(i));

                var perServing = totalProtein / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateCarbohydrates(RecipeDto recipe)
        {
            try
            {
                var totalCarbohydrates = recipe.Ingredients
                    .Sum(i => ConvertToCarbohydrates(i));

                var perServing = totalCarbohydrates / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateFat(RecipeDto recipe)
        {
            try
            {
                var totalFat = recipe.Ingredients
                    .Sum(i => ConvertToFat(i));

                var perServing = totalFat / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public decimal ConvertToCalories(IngredientDto ingredient)
        {
            decimal caloriesPerUnit = ingredient.FoodItem?.CaloriesPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Calories per gram
            return quantityInGrams * (caloriesPerUnit / 100);
        }

        public decimal ConvertToProtein(IngredientDto ingredient)
        {
            decimal proteinPerUnit = ingredient.FoodItem?.ProteinPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Protein per gram
            return quantityInGrams * (proteinPerUnit / 100);
        }

        public decimal ConvertToCarbohydrates(IngredientDto ingredient)
        {
            decimal carbohydratesPerUnit = ingredient.FoodItem?.CarbohydratesPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Carbohydrates per gram
            return quantityInGrams * (carbohydratesPerUnit / 100);
        }

        public decimal ConvertToFat(IngredientDto ingredient)
        {
            decimal fatPerUnit = ingredient.FoodItem?.FatPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Fat per gram
            return quantityInGrams * (fatPerUnit / 100);
        }

        private decimal GetAverageWeightPerPiece(string foodItemName)
        {
            // Define average weights for specific food items
            return foodItemName switch
            {
                "Bell Pepper" => 120,
                "Carrot" => 75,
                "Banana" => 120,
                "Onion" => 110,
                "Garlic" => 5,
                _ => 100 // Default weight
            };
        }
    }
}

```

## File: RecipeUseCases.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Application.Interfaces;

namespace MyFoodApp.Application.UseCases
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

```

