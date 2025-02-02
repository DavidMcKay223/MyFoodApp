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
