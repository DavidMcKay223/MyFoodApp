# Directory: Entities

## File: FoodCategory.cs

```C#
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class FoodCategory
    {
        [Key]
        [JsonIgnore]
        public int FoodCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<FoodItem> FoodItems { get; set; } = [];
    }
}

```

## File: FoodItem.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyFoodApp.Domain.Enums;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItem
    {
        [Key]
        [JsonIgnore]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        [ForeignKey("FoodCategory")]
        public int FoodCategoryId { get; set; }

        // Nutritional information (simplified)
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CaloriesPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ProteinPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CarbohydratesPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FatPerUnit { get; set; }
        public UnitType Unit { get; set; }

        // Navigation
        [JsonIgnore]
        public FoodCategory? FoodCategory { get; set; }

        [JsonIgnore]
        public ICollection<PriceHistory> PriceHistories { get; set; } = [];

        [JsonIgnore]
        public ICollection<FoodItemStoreSection> StoreSections { get; set; } = [];

        [JsonIgnore]
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}

```

## File: FoodItemStoreSection.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItemStoreSection
    {
        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [ForeignKey("StoreSection")]
        public int StoreSectionId { get; set; }

        // Additional metadata if needed
        public int? ShelfNumber { get; set; }

        // Navigation
        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }

        [JsonIgnore]
        public StoreSection? StoreSection { get; set; }
    }
}

```

## File: Ingredient.cs

```C#
using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        [JsonIgnore]
        public int IngredientId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }

        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }
    }
}

```

## File: MealSuggestion.cs

```C#
using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestion
    {
        [Key]
        [JsonIgnore]
        public int MealSuggestionId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        public MealType MealType { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<RecipeMealSuggestion> RecipeSuggestions { get; set; } = [];

        [JsonIgnore]
        public ICollection<MealSuggestionTag> Tags { get; set; } = [];
    }
}

```

## File: MealSuggestionTag.cs

```C#
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestionTag
    {
        [Key]
        [JsonIgnore]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TagName { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<MealSuggestion> MealSuggestions { get; set; } = [];
    }
}

```

## File: PriceHistory.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class PriceHistory
    {
        [Key]
        [JsonIgnore]
        public int PriceHistoryId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation
        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }
    }
}

```

## File: Recipe.cs

```C#
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class Recipe
    {
        [Key]
        [JsonIgnore]
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [MaxLength(2000)]
        public required string Description { get; set; }

        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<Ingredient> Ingredients { get; set; } = [];

        [JsonIgnore]
        public ICollection<RecipeStep> Steps { get; set; } = [];

        [JsonIgnore]
        public ICollection<RecipeMealSuggestion> MealSuggestions { get; set; } = [];
    }
}

```

## File: RecipeMealSuggestion.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeMealSuggestion
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("MealSuggestion")]
        public int MealSuggestionId { get; set; }

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }

        [JsonIgnore]
        public MealSuggestion? MealSuggestion { get; set; }
    }
}

```

## File: RecipeStep.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeStep
    {
        [Key]
        [JsonIgnore]
        public int StepId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public int StepNumber { get; set; }

        [MaxLength(1000)]
        public required string Instruction { get; set; }

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}

```

## File: StoreSection.cs

```C#
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class StoreSection
    {
        [Key]
        [JsonIgnore]
        public int StoreSectionId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<FoodItemStoreSection> FoodItems { get; set; } = [];
    }
}

```

