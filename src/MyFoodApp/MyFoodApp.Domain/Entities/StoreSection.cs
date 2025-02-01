using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class StoreSection
    {
        [Key]
        public int StoreSectionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Aisle 3", "Dairy Section"

        [MaxLength(500)]
        public string Description { get; set; }

        // Navigation
        public ICollection<FoodItemStoreSection> FoodItems { get; set; }
    }
}
