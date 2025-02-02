using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestionTag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TagName { get; set; }

        // Navigation
        public ICollection<MealSuggestion> MealSuggestions { get; set; } = [];
    }
}
