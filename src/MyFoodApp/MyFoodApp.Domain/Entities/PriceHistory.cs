using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class PriceHistory
    {
        [Key]
        [JsonIgnore]
        public int PriceHistoryId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation
        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }
    }
}
