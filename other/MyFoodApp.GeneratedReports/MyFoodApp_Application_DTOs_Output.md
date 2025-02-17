# Directory: DTOs

## File: FoodCategoryDto.cs

```C#
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class FoodCategoryDto
    {
        public int FoodCategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

```

## File: FoodItemDto.cs

```C#
using MyFoodApp.Domain.Enums;
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class FoodItemDto
    {
        public int FoodItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int FoodCategoryId { get; set; }
        public decimal? CaloriesPerUnit { get; set; }
        public decimal? ProteinPerUnit { get; set; }
        public decimal? CarbohydratesPerUnit { get; set; }
        public decimal? FatPerUnit { get; set; }
        public UnitType Unit { get; set; }

        // Navigation Properties
        public FoodCategoryDto? FoodCategory { get; set; }
    }
}

```

## File: FoodItemSearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class FoodItemSearchDto : SearchDto<FoodItemSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? FoodCategoryId { get; set; }
        public decimal? MinCaloriesPerUnit { get; set; }
        public decimal? MaxCaloriesPerUnit { get; set; }
        public decimal? MinProteinPerUnit { get; set; }
        public decimal? MaxProteinPerUnit { get; set; }
    }
}

```

## File: FoodItemStoreSectionDto.cs

```C#
namespace MyFoodApp.Application.DTOs
{
    public class FoodItemStoreSectionDto
    {
        public int FoodItemId { get; set; }
        public int StoreSectionId { get; set; }
        public int? ShelfNumber { get; set; }

        // Navigation Properties
        public FoodItemDto? FoodItem { get; set; }
        public StoreSectionDto? StoreSection { get; set; }
    }
}

```

## File: IngredientDto.cs

```C#
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public int FoodItemId { get; set; }
        public decimal Quantity { get; set; }
        public UnitType Unit { get; set; }

        // Navigation Properties
        //public RecipeDto? Recipe { get; set; }
        public FoodItemDto? FoodItem { get; set; }
    }
}

```

## File: IngredientSearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class IngredientSearchDto : SearchDto<IngredientSortBy, SortOrder>
    {
        public int? RecipeId { get; set; }
        public int? FoodItemId { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public UnitType? Unit { get; set; }
    }

}

```

## File: MealSuggestionDto.cs

```C#
using MyFoodApp.Domain.Enums;
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionDto
    {
        public int MealSuggestionId { get; set; }
        public required string Name { get; set; }
        public MealType MealType { get; set; }
        public required string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public List<RecipeMealSuggestionDto> RecipeSuggestions { get; set; } = [];

        [JsonIgnore]
        public List<MealSuggestionTagDto> Tags { get; set; } = [];
    }
}

```

## File: MealSuggestionSearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionSearchDto : SearchDto<MealSuggestionSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public MealType? MealType { get; set; }
        public string? Description { get; set; }
        public DateTime? EffectiveDateFrom { get; set; }
        public DateTime? EffectiveDateTo { get; set; }
        public DateTime? ExpirationDateFrom { get; set; }
        public DateTime? ExpirationDateTo { get; set; }
    }
}

```

## File: MealSuggestionTagDto.cs

```C#
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionTagDto
    {
        public int TagId { get; set; }
        public required string TagName { get; set; }

        // Navigation Property
        [JsonIgnore]
        public List<MealSuggestionDto> MealSuggestions { get; set; } = [];
    }
}

```

## File: PriceHistoryDto.cs

```C#
namespace MyFoodApp.Application.DTOs
{
    public class PriceHistoryDto
    {
        public int PriceHistoryId { get; set; }
        public int FoodItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation Property
        public FoodItemDto? FoodItem { get; set; }
    }
}

```

## File: PriceHistorySearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class PriceHistorySearchDto : SearchDto<PriceHistorySortBy, SortOrder>
    {
        public int? FoodItemId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
    }
}

```

## File: RecipeDto.cs

```C#
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }

        // Navigation Properties
        public List<RecipeStepDto> Steps { get; set; } = [];

        public List<IngredientDto> Ingredients { get; set; } = [];

        public List<RecipeMealSuggestionDto> MealSuggestions { get; set; } = [];
    }
}

```

## File: RecipeMealSuggestionDto.cs

```C#
namespace MyFoodApp.Application.DTOs
{
    public class RecipeMealSuggestionDto
    {
        public int RecipeId { get; set; }
        public int MealSuggestionId { get; set; }

        // Navigation Properties
        public RecipeDto? Recipe { get; set; }
        public MealSuggestionDto? MealSuggestion { get; set; }
    }
}

```

## File: RecipeSearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class RecipeSearchDto : SearchDto<RecipeSortBy, SortOrder>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? PrepTimeMin { get; set; }
        public int? PrepTimeMax { get; set; }
        public int? CookTimeMin { get; set; }
        public int? CookTimeMax { get; set; }
        public int? ServingsMin { get; set; }
        public int? ServingsMax { get; set; }
    }
}

```

## File: RecipeStepDto.cs

```C#
namespace MyFoodApp.Application.DTOs
{
    public class RecipeStepDto
    {
        public int StepId { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public required string Instruction { get; set; }
    }
}

```

## File: StoreSectionDto.cs

```C#
using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionDto
    {
        public int StoreSectionId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

```

## File: StoreSectionSearchDto.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionSearchDto : SearchDto<StoreSectionSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

```

