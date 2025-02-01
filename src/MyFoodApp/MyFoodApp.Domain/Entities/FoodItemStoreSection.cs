using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class FoodItemStoreSection
    {
        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [ForeignKey("StoreSection")]
        public int StoreSectionId { get; set; }

        // Additional metadata if needed
        public int? ShelfNumber { get; set; }

        // Navigation
        public FoodItem FoodItem { get; set; }
        public StoreSection StoreSection { get; set; }
    }
}
