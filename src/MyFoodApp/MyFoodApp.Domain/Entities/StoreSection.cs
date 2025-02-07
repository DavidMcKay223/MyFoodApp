using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class StoreSection
    {
        [Key]
        [JsonIgnore]
        public int StoreSectionId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        [JsonIgnore]
        public ICollection<FoodItemStoreSection> FoodItems { get; set; } = [];
    }
}
