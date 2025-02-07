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
