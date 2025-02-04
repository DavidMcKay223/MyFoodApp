# Directory: Entities

## File: FoodCategory.cs

```C#
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class FoodCategory
    {
        [Key]
        public int FoodCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        public ICollection<FoodItem> FoodItems { get; set; } = [];
    }
}

```

## File: FoodItem.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItem
    {
        [Key]
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

        // Navigation
        public FoodCategory? FoodCategory { get; set; }
        public ICollection<PriceHistory> PriceHistories { get; set; } = [];
        public ICollection<FoodItemStoreSection> StoreSections { get; set; } = [];
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}

```

## File: FoodItemStoreSection.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;

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
        public FoodItem? FoodItem { get; set; }
        public StoreSection? StoreSection { get; set; }
    }
}

```

## File: Ingredient.cs

```C#
using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
        public FoodItem? FoodItem { get; set; }
    }
}

```

## File: MealSuggestion.cs

```C#
using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestion
    {
        [Key]
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
        public ICollection<RecipeMealSuggestion> RecipeSuggestions { get; set; } = [];
        public ICollection<MealSuggestionTag> Tags { get; set; } = [];
    }
}

```

## File: MealSuggestionTag.cs

```C#
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestionTag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TagName { get; set; }

        // Navigation
        public ICollection<MealSuggestion> MealSuggestions { get; set; } = [];
    }
}

```

## File: PriceHistory.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class PriceHistory
    {
        [Key]
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
        public FoodItem? FoodItem { get; set; }
    }
}

```

## File: Recipe.cs

```C#
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class Recipe
    {
        [Key]
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
        public ICollection<Ingredient> Ingredients { get; set; } = [];
        public ICollection<RecipeStep> Steps { get; set; } = [];
        public ICollection<RecipeMealSuggestion> MealSuggestions { get; set; } = [];
    }
}

```

## File: RecipeMealSuggestion.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeMealSuggestion
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("MealSuggestion")]
        public int MealSuggestionId { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
        public MealSuggestion? MealSuggestion { get; set; }
    }
}

```

## File: RecipeStep.cs

```C#
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeStep
    {
        [Key]
        public int StepId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public int StepNumber { get; set; }

        [MaxLength(1000)]
        public required string Instruction { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
    }
}

```

## File: StoreSection.cs

```C#
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class StoreSection
    {
        [Key]
        public int StoreSectionId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        public ICollection<FoodItemStoreSection> FoodItems { get; set; } = [];
    }
}

```

