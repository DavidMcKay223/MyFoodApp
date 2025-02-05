using MyFoodApp.Application.Interfaces.Calculations;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.UseCases.Calculations
{
    public class NutritionCalculatorService : INutritionCalculatorService
    {
        public string CalculateCalories(RecipeDto recipe)
        {
            try
            {
                var totalCalories = recipe.Ingredients
                    .Sum(i => ConvertToCalories(i));

                var perServing = totalCalories / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateProtein(RecipeDto recipe)
        {
            try
            {
                var totalProtein = recipe.Ingredients
                    .Sum(i => ConvertToProtein(i));

                var perServing = totalProtein / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateCarbohydrates(RecipeDto recipe)
        {
            try
            {
                var totalCarbohydrates = recipe.Ingredients
                    .Sum(i => ConvertToCarbohydrates(i));

                var perServing = totalCarbohydrates / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public string CalculateFat(RecipeDto recipe)
        {
            try
            {
                var totalFat = recipe.Ingredients
                    .Sum(i => ConvertToFat(i));

                var perServing = totalFat / recipe.Servings;
                return perServing > 0 ? perServing.ToString("N0") : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public decimal ConvertToCalories(IngredientDto ingredient)
        {
            decimal caloriesPerUnit = ingredient.FoodItem?.CaloriesPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Calories per gram
            return quantityInGrams * (caloriesPerUnit / 100);
        }

        public decimal ConvertToProtein(IngredientDto ingredient)
        {
            decimal proteinPerUnit = ingredient.FoodItem?.ProteinPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Protein per gram
            return quantityInGrams * (proteinPerUnit / 100);
        }

        public decimal ConvertToCarbohydrates(IngredientDto ingredient)
        {
            decimal carbohydratesPerUnit = ingredient.FoodItem?.CarbohydratesPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Carbohydrates per gram
            return quantityInGrams * (carbohydratesPerUnit / 100);
        }

        public decimal ConvertToFat(IngredientDto ingredient)
        {
            decimal fatPerUnit = ingredient.FoodItem?.FatPerUnit ?? 0;
            decimal quantityInGrams;

            switch (ingredient.Unit)
            {
                case UnitType.Gram:
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Kilogram:
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Milliliter:
                    // Assuming density of water, 1 milliliter = 1 gram
                    quantityInGrams = ingredient.Quantity;
                    break;
                case UnitType.Liter:
                    // Assuming density of water, 1 liter = 1000 grams
                    quantityInGrams = ingredient.Quantity * 1000;
                    break;
                case UnitType.Teaspoon:
                    // Assuming 1 teaspoon = 5 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 5;
                    break;
                case UnitType.Tablespoon:
                    // Assuming 1 tablespoon = 15 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 15;
                    break;
                case UnitType.Cup:
                    // Assuming 1 cup = 240 grams (adjust based on substance)
                    quantityInGrams = ingredient.Quantity * 240;
                    break;
                case UnitType.Piece:
                    // Assuming average weight per piece for certain food items
                    quantityInGrams = ingredient.Quantity * GetAverageWeightPerPiece(ingredient.FoodItem!.Name);
                    break;
                default:
                    quantityInGrams = ingredient.Quantity;
                    break;
            }

            // Fat per gram
            return quantityInGrams * (fatPerUnit / 100);
        }

        private decimal GetAverageWeightPerPiece(string foodItemName)
        {
            // Define average weights for specific food items
            return foodItemName switch
            {
                "Bell Pepper" => 120,
                "Carrot" => 75,
                "Banana" => 120,
                "Onion" => 110,
                "Garlic" => 5,
                _ => 100 // Default weight
            };
        }
    }
}
