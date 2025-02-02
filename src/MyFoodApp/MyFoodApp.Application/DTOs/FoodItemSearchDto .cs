using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class FoodItemSearchDto : SearchDto<FoodItemSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? FoodCategoryId { get; set; }
        public decimal? MinCaloriesPerUnit { get; set; }
        public decimal? MaxCaloriesPerUnit { get; set; }
        public decimal? MinProteinPerUnit { get; set; }
        public decimal? MaxProteinPerUnit { get; set; }
    }
}
