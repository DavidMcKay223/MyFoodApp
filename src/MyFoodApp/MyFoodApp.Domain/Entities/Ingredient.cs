using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
        public FoodItem? FoodItem { get; set; }
    }
}
