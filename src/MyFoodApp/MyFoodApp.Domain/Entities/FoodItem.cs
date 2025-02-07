using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyFoodApp.Domain.Enums;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItem
    {
        [Key]
        [JsonIgnore]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        [ForeignKey("FoodCategory")]
        public int FoodCategoryId { get; set; }

        // Nutritional information (simplified)
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CaloriesPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ProteinPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CarbohydratesPerUnit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FatPerUnit { get; set; }
        public UnitType Unit { get; set; }

        // Navigation
        [JsonIgnore]
        public FoodCategory? FoodCategory { get; set; }

        [JsonIgnore]
        public ICollection<PriceHistory> PriceHistories { get; set; } = [];

        [JsonIgnore]
        public ICollection<FoodItemStoreSection> StoreSections { get; set; } = [];

        [JsonIgnore]
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}
