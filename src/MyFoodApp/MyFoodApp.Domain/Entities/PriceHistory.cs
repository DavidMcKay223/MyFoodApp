using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class PriceHistory
    {
        [Key]
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
        public FoodItem FoodItem { get; set; }
    }
}
