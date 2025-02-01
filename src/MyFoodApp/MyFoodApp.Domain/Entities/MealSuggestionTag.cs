using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestionTag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TagName { get; set; }

        // Navigation
        public ICollection<MealSuggestion> MealSuggestions { get; set; }
    }
}
