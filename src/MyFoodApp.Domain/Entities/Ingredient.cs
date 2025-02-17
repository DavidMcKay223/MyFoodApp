using MyFoodApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        [JsonIgnore]
        public int IngredientId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }

        [JsonIgnore]
        public FoodItem? FoodItem { get; set; }
    }
}
