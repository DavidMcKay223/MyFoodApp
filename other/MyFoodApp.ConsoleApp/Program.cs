using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Enums;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            //ResetDatabase();
            //SeedTestData();
        }

        public static void ResetDatabase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyFoodApp;Trusted_Connection=True;")
                    .Options;

            using (var context = new AppDbContext(options))
            {
                // Drop the database if it exists
                context.Database.EnsureDeleted();

                // Recreate the database
                context.Database.EnsureCreated();
            }

            Console.WriteLine("Database has been reset successfully!");
        }

        public static void SeedTestData()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyFoodApp;Trusted_Connection=True;")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();

                // Food Categories
                var foodCategories = new List<FoodCategory>
                {
                    new() { Name = "Vegetables", Description = "Fresh vegetables" },
                    new() { Name = "Fruits", Description = "Seasonal fruits" },
                    new() { Name = "Dairy", Description = "Milk, cheese, and other dairy products" },
                    new() { Name = "Grains", Description = "Bread, rice, and cereals" },
                    new() { Name = "Meat", Description = "Poultry and red meat" }
                };
                context.FoodCategories.AddRange(foodCategories);
                context.SaveChanges();

                // Store Sections
                var storeSections = new List<StoreSection>
                {
                    new() { Name = "Produce", Description = "Fresh fruits and vegetables" },
                    new() { Name = "Dairy Aisle", Description = "Milk and cheese products" },
                    new() { Name = "Bakery", Description = "Bread and pastries" },
                    new() { Name = "Meat Section", Description = "Fresh and frozen meats" }
                };
                context.StoreSections.AddRange(storeSections);
                context.SaveChanges();

                // Food Items
                var foodItems = new List<FoodItem>
                {
                    new() {
                        Name = "Carrot", Description = "Orange root vegetable",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Vegetables").FoodCategoryId,
                        CaloriesPerUnit = 41, ProteinPerUnit = 0.9M
                    },
                    new() {
                        Name = "Apple", Description = "Red or green fruit",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Fruits").FoodCategoryId,
                        CaloriesPerUnit = 52, ProteinPerUnit = 0.3M
                    },
                    new() {
                        Name = "Milk", Description = "Dairy product",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Dairy").FoodCategoryId,
                        CaloriesPerUnit = 42, ProteinPerUnit = 3.4M
                    },
                    new() {
                        Name = "Chicken Breast", Description = "Boneless skinless chicken",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Meat").FoodCategoryId,
                        CaloriesPerUnit = 165, ProteinPerUnit = 31M
                    },
                    new() {
                        Name = "Whole Wheat Bread", Description = "High fiber bread",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Grains").FoodCategoryId,
                        CaloriesPerUnit = 247, ProteinPerUnit = 13M
                    }
                };
                context.FoodItems.AddRange(foodItems);
                context.SaveChanges();

                // Food Item Store Sections
                var foodItemSections = new List<FoodItemStoreSection>
                {
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Carrot").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 3
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Milk").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Dairy Aisle").StoreSectionId,
                        ShelfNumber = 2
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Chicken Breast").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Meat Section").StoreSectionId
                    }
                };
                context.FoodItemStoreSections.AddRange(foodItemSections);
                context.SaveChanges();

                // Price Histories
                var priceHistories = new List<PriceHistory>
                {
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Milk").FoodItemId,
                        Price = 2.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 3, 31)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Milk").FoodItemId,
                        Price = 3.49M,
                        StartDate = new DateTime(2023, 4, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Chicken Breast").FoodItemId,
                        Price = 5.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    }
                };
                context.PriceHistories.AddRange(priceHistories);
                context.SaveChanges();

                // Recipes
                var recipes = new List<Recipe>
                {
                    new() {
                        Title = "Carrot Salad",
                        Description = "A simple and healthy carrot salad",
                        PrepTimeMinutes = 15,
                        CookTimeMinutes = 0,
                        Servings = 4
                    },
                    new() {
                        Title = "Apple Pie",
                        Description = "A delicious apple pie",
                        PrepTimeMinutes = 30,
                        CookTimeMinutes = 45,
                        Servings = 8
                    },
                    new() {
                        Title = "Chicken Sandwich",
                        Description = "Grilled chicken sandwich",
                        PrepTimeMinutes = 10,
                        CookTimeMinutes = 15,
                        Servings = 2
                    }
                };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Ingredients
                var ingredients = new List<Ingredient>
                {
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Carrot Salad").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Carrot").FoodItemId,
                        Quantity = 200,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Apple Pie").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Apple").FoodItemId,
                        Quantity = 500,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken Sandwich").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Chicken Breast").FoodItemId,
                        Quantity = 2,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken Sandwich").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Whole Wheat Bread").FoodItemId,
                        Quantity = 4,
                        Unit = UnitType.Piece
                    }
                };
                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();

                // Recipe Steps
                var recipeSteps = new List<RecipeStep>
                {
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Carrot Salad").RecipeId,
                        StepNumber = 1,
                        Instruction = "Peel and grate the carrots."
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Carrot Salad").RecipeId,
                        StepNumber = 2,
                        Instruction = "Mix with lemon juice and olive oil."
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken Sandwich").RecipeId,
                        StepNumber = 1,
                        Instruction = "Grill the chicken breast for 6-8 minutes per side."
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken Sandwich").RecipeId,
                        StepNumber = 2,
                        Instruction = "Assemble sandwich with bread and preferred condiments."
                    }
                };
                context.RecipeSteps.AddRange(recipeSteps);
                context.SaveChanges();

                // Meal Suggestions
                var mealSuggestions = new List<MealSuggestion>
                {
                    new() {
                        Name = "Healthy Breakfast",
                        MealType = MealType.Breakfast,
                        Description = "Low calorie morning meal",
                        EffectiveDate = new DateTime(2023, 1, 1),
                        ExpirationDate = new DateTime(2024, 1, 1)
                    },
                    new() {
                        Name = "Quick Lunch",
                        MealType = MealType.Lunch,
                        Description = "Fast and nutritious lunch",
                        EffectiveDate = new DateTime(2023, 1, 1)
                    }
                };
                context.MealSuggestions.AddRange(mealSuggestions);
                context.SaveChanges();

                // Meal Suggestion Tags
                var tags = new List<MealSuggestionTag>
                {
                    new() { TagName = "Quick" },
                    new() { TagName = "Vegetarian" },
                    new() { TagName = "High-Protein" }
                };
                context.MealSuggestionTags.AddRange(tags);
                context.SaveChanges();

                // Recipe-Meal Suggestion Relationships
                var recipeMealSuggestions = new List<RecipeMealSuggestion>
                {
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken Sandwich").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Quick Lunch").MealSuggestionId
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Carrot Salad").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Healthy Breakfast").MealSuggestionId
                    }
                };
                context.RecipeMealSuggestions.AddRange(recipeMealSuggestions);
                context.SaveChanges();

                Console.WriteLine("Test data populated successfully!");
            }
        }
    }
}
