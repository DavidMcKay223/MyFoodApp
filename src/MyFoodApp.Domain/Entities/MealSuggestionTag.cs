using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestionTag
    {
        [Key]
        [JsonIgnore]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TagName { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<MealSuggestion> MealSuggestions { get; set; } = [];
    }
}
