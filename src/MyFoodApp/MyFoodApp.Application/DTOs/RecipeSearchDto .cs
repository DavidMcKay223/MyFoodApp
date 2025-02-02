using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class RecipeSearchDto : SearchDto<RecipeSortBy, SortOrder>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? PrepTimeMin { get; set; }
        public int? PrepTimeMax { get; set; }
        public int? CookTimeMin { get; set; }
        public int? CookTimeMax { get; set; }
        public int? ServingsMin { get; set; }
        public int? ServingsMax { get; set; }
    }
}
