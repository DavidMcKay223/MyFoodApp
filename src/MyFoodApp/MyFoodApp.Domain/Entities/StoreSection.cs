using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Domain.Entities
{
    public class StoreSection
    {
        [Key]
        public int StoreSectionId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        // Navigation
        public ICollection<FoodItemStoreSection> FoodItems { get; set; } = [];
    }
}
