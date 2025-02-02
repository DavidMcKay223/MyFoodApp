using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionSearchDto : SearchDto<StoreSectionSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
