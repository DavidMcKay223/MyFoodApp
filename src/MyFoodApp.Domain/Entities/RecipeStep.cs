using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeStep
    {
        [Key]
        [JsonIgnore]
        public int StepId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public int StepNumber { get; set; }

        [MaxLength(1000)]
        public required string Instruction { get; set; }

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}
