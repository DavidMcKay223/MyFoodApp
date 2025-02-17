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
