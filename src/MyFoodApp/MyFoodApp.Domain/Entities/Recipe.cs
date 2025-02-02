using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<RecipeStep> Steps { get; set; }
        public ICollection<RecipeMealSuggestion> MealSuggestions { get; set; }
    }
}
