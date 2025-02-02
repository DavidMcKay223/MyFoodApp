using MyFoodApp.Application.Common;
using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class IngredientSearchDto : SearchDto<IngredientSortBy, SortOrder>
    {
        public int? RecipeId { get; set; }
        public int? FoodItemId { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public UnitType? Unit { get; set; }
    }

}
