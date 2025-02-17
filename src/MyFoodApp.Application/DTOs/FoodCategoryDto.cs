using Newtonsoft.Json;

namespace MyFoodApp.Application.DTOs
{
    public class FoodCategoryDto
    {
        public int FoodCategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
