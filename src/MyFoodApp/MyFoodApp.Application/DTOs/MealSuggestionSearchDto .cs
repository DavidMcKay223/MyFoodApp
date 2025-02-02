using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionSearchDto : SearchDto<MealSuggestionSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public MealType? MealType { get; set; }
        public string? Description { get; set; }
        public DateTime? EffectiveDateFrom { get; set; }
        public DateTime? EffectiveDateTo { get; set; }
        public DateTime? ExpirationDateFrom { get; set; }
        public DateTime? ExpirationDateTo { get; set; }
    }
}
