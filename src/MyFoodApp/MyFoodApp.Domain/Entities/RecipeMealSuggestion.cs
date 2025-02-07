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
