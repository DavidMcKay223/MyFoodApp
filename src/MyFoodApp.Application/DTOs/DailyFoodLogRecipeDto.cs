using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class DailyFoodLogRecipeDto
    {
        public int ID { get; set; }
        public int LogId { get; set; }
        public int RecipeId { get; set; }

        // Navigation Properties
        public DailyFoodLogDto? DailyFoodLog { get; set; }
        public RecipeDto? Recipe { get; set; }
    }
}
