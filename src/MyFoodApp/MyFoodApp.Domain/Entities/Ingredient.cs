using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        // Navigation
        public Recipe? Recipe { get; set; }
        public FoodItem? FoodItem { get; set; }
    }
}
