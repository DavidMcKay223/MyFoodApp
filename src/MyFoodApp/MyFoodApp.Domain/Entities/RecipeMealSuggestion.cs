using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
