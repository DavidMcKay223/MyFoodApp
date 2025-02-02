using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionTagDto
    {
        public int TagId { get; set; }
        public required string TagName { get; set; }

        // Navigation Property
        public List<MealSuggestionDto> MealSuggestions { get; set; } = [];
    }
}
