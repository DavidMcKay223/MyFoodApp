# Directory: Interfaces

## File: IFoodItemUseCases.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IFoodItemUseCases
    {
        Task<Response<FoodItemDto>> GetFoodItemByIdAsync(int foodItemId);
        Task<Response<FoodItemDto>> GetFoodItemListAsync();
    }
}

```

## File: IGenerateRecommendationsUseCases.cs

```C#
namespace MyFoodApp.Application.Interfaces
{
    public interface IGenerateRecommendationsUseCases
    {
    }
}

```

## File: IMealSuggestionUseCases.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IMealSuggestionUseCases
    {
        Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId);
        Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync();
    }
}

```

## File: INutritionCalculatorService.cs

```C#
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface INutritionCalculatorService
    {
        string CalculateCalories(RecipeDto recipe);
        decimal ConvertToCalories(IngredientDto ingredient);
        string CalculateProtein(RecipeDto recipe);
        decimal ConvertToProtein(IngredientDto ingredient);
        string CalculateCarbohydrates(RecipeDto recipe);
        decimal ConvertToCarbohydrates(IngredientDto ingredient);
        string CalculateFat(RecipeDto recipe);
        decimal ConvertToFat(IngredientDto ingredient);
    }
}

```

## File: IRecipeUseCases.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Interfaces
{
    public interface IRecipeUseCases
    {
        Task<Response<RecipeDto>> LookupRecipesAsync(RecipeSearchDto searchDto);
        Task<Response<RecipeDto>> SuggestRecipesBasedOnIngredientsAsync(IEnumerable<int> foodItemIds);
        Task<Response<RecipeDto>> GetRecipeByIdAsync(int recipeId);
        Task<Response<RecipeDto>> CreateRecipeAsync(RecipeDto recipeDto);
        Task<Response<RecipeDto>> UpdateRecipeAsync(int recipeId, RecipeDto recipeDto);
        Task<Response<RecipeDto>> DeleteRecipeAsync(int recipeId);
    }
}

```

