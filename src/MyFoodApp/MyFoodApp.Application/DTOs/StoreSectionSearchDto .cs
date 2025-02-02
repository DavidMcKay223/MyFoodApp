using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionSearchDto : SearchDto<StoreSectionSortBy, SortOrder>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
