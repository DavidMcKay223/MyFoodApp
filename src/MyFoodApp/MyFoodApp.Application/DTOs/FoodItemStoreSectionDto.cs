using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class FoodItemStoreSectionDto
    {
        public int FoodItemId { get; set; }
        public int StoreSectionId { get; set; }
        public int? ShelfNumber { get; set; }

        // Navigation Properties
        public FoodItemDto? FoodItem { get; set; }
        public StoreSectionDto? StoreSection { get; set; }
    }
}
