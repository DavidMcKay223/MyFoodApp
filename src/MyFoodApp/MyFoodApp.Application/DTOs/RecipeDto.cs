using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<RecipeStepDto> Steps { get; set; } = [];
        public List<IngredientDto> Ingredients { get; set; } = [];
        public List<RecipeMealSuggestionDto> MealSuggestions { get; set; } = [];
    }
}
