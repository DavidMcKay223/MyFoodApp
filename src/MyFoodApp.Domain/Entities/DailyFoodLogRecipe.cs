using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class DailyFoodLogRecipe
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("DailyFoodLog")]
        public int LogId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public DailyFoodLog? DailyFoodLog { get; set; }

        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}
