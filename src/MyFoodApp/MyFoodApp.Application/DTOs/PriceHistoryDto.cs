using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class PriceHistoryDto
    {
        public int PriceHistoryId { get; set; }
        public int FoodItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation Property
        public FoodItemDto? FoodItem { get; set; }
    }
}
