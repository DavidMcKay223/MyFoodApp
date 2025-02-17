using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class FoodCategory
    {
        [Key]
        [JsonIgnore]
        public int FoodCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<FoodItem> FoodItems { get; set; } = [];
    }
}
