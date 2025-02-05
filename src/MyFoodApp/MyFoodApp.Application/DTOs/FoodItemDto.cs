using MyFoodApp.Domain.Enums;

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
        public decimal? CarbohydratesPerUnit { get; set; }
        public decimal? FatPerUnit { get; set; }
        public UnitType Unit { get; set; }

        // Navigation Properties
        public FoodCategoryDto? FoodCategory { get; set; }
        public List<PriceHistoryDto> PriceHistories { get; set; } = [];
        public List<FoodItemStoreSectionDto> StoreSections { get; set; } = [];
        public List<IngredientDto> Ingredients { get; set; } = [];
    }
}
