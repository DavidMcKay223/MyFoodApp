# Directory: Interfaces\Repositories

## File: IFoodItemRepository.cs

```C#
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IFoodItemRepository
    {
        IQueryable<FoodItem> GetAllFoodItemsAsync();
        Task<FoodItem?> GetFoodItemByIdAsync(int foodItemId, bool tracking = false);
    }
}

```

## File: IGenerateRecommendationsRepository.cs

```C#
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IGenerateRecommendationsRepository
    {
        IQueryable<MealSuggestionTag> GetAllMealSuggestionsTagsAsync();
    }
}

```

## File: IGeneratorPdfRepository.cs

```C#
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IGeneratorPdfRepository
    {
        Task<List<Recipe>> GetAllRecipesByIdListAsync(List<int> idList);
    }
}

```

## File: IMealSuggestionRepository.cs

```C#
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IMealSuggestionRepository
    {
        IQueryable<MealSuggestion> GetAllMealSuggestionsAsync();
        Task<MealSuggestion?> GetMealSuggestionByIdAsync(int foodItemId, bool tracking = false);
    }
}

```

## File: IRecipeRepository.cs

```C#
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int recipeId);
        Task<Recipe?> GetRecipeByIdAsync(int recipeId, bool tracking);
        Task<Recipe> AddRecipeAsync(Recipe recipe);
        Task AddRecipeStepRangeAsync(List<RecipeStep> items);
        Task AddIngredientRangeAsync(List<Ingredient> items);
        Task AddRecipeMealSuggestionRangeAsync(List<RecipeMealSuggestion> items);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Recipe recipe);
        IQueryable<Recipe> GetRecipesByIngredientsAsync(IEnumerable<int> foodItemIds);
        Task DeleteIngredientAsync(Ingredient ingredient);
        Task DeleteStepAsync(RecipeStep step);
        Task DeleteMealSuggestionAsync(RecipeMealSuggestion suggestion);
    }
}

```

