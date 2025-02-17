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
