using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public int FoodItemId { get; set; }
        public decimal Quantity { get; set; }
        public UnitType Unit { get; set; }
    }
}
