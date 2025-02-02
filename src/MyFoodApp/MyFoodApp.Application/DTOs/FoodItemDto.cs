using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class FoodItemDto
    {
        public int FoodItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int FoodCategoryId { get; set; }
        public decimal? CaloriesPerUnit { get; set; }
        public decimal? ProteinPerUnit { get; set; }

        // Navigation Properties
        public FoodCategoryDto? FoodCategory { get; set; }
        public List<PriceHistoryDto> PriceHistories { get; set; } = [];
        public List<FoodItemStoreSectionDto> StoreSections { get; set; } = [];
        public List<IngredientDto> Ingredients { get; set; } = [];
    }
}
