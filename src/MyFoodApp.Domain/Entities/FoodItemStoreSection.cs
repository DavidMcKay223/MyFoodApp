using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }

        [JsonIgnore]
        public StoreSection? StoreSection { get; set; }
    }
}
