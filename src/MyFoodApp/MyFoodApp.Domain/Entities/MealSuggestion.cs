using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestion
    {
        [Key]
        public int MealSuggestionId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        public MealType MealType { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // Navigation
        public ICollection<RecipeMealSuggestion> RecipeSuggestions { get; set; } = [];
        public ICollection<MealSuggestionTag> Tags { get; set; } = [];
    }
}
