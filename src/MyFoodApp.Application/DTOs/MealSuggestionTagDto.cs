using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionTagDto
    {
        public int TagId { get; set; }
        public required string TagName { get; set; }

        // Navigation Property
        [JsonIgnore]
        public List<MealSuggestionDto> MealSuggestions { get; set; } = [];
    }
}
