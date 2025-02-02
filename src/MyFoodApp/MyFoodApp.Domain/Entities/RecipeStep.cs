using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class RecipeStep
    {
        [Key]
        public int StepId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public int StepNumber { get; set; }

        [MaxLength(1000)]
        public required string Instruction { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
    }
}
