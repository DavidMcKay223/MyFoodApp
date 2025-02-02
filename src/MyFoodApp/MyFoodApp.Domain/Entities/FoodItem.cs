using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        [ForeignKey("FoodCategory")]
        public int FoodCategoryId { get; set; }

        // Nutritional information (simplified)
        public decimal? CaloriesPerUnit { get; set; }
        public decimal? ProteinPerUnit { get; set; }

        // Navigation
        public FoodCategory? FoodCategory { get; set; }
        public ICollection<PriceHistory> PriceHistories { get; set; } = [];
        public ICollection<FoodItemStoreSection> StoreSections { get; set; } = [];
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}
