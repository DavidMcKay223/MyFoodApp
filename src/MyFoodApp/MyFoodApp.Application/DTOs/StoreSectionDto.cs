namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionDto
    {
        public int StoreSectionId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // Navigation Property
        public List<FoodItemStoreSectionDto>? FoodItems { get; set; }
    }
}
