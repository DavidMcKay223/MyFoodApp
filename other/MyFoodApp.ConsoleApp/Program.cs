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
                //context.Database.EnsureCreated();
                context.Database.Migrate();
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
                    new() { Name = "Vegetables", Description = "All forms of vegetables including leafy greens, roots, and cruciferous" },
                    new() { Name = "Fruits", Description = "Fresh, dried, and preserved fruits" },
                    new() { Name = "Dairy & Alternatives", Description = "Animal dairy products and plant-based alternatives" },
                    new() { Name = "Grains & Cereals", Description = "Wheat, rice, oats, barley, and derived products" },
                    new() { Name = "Proteins", Description = "Animal and plant-based protein sources" },
                    new() { Name = "Seafood", Description = "Fish, shellfish, and aquatic foods" },
                    new() { Name = "Baked Goods", Description = "Bread, pastries, and other bakery items" },
                    new() { Name = "Condiments & Sauces", Description = "Spreads, dressings, and flavor enhancers" },
                    new() { Name = "Snacks & Sweets", Description = "Processed snacks, candies, and desserts" },
                    new() { Name = "Beverages", Description = "All drinkable liquids excluding plain water" },
                    new() { Name = "Fats & Oils", Description = "Cooking oils, butter, and lipid-based products" },
                    new() { Name = "Prepared Foods", Description = "Ready-to-eat meals and processed food items" },
                    new() { Name = "Herbs & Spices", Description = "Culinary flavorings and seasoning agents" },
                    new() { Name = "Legumes", Description = "Beans, lentils, peas, and related plants" },
                    new() { Name = "Nuts & Seeds", Description = "Tree nuts, peanuts, and edible seeds" },
                    new() { Name = "Fermented Foods", Description = "Kimchi, yogurt, sauerkraut, and cultured products" },
                    new() { Name = "Frozen Foods", Description = "Flash-frozen fruits, vegetables, and meals" },
                    new() { Name = "Canned/Preserved", Description = "Long-shelf-life packaged foods" },
                    new() { Name = "International", Description = "Ethnic-specific food ingredients" },
                    new() { Name = "Special Diets", Description = "Gluten-free, vegan, keto, and allergy-friendly items" }
                };

                context.FoodCategories.AddRange(foodCategories);
                context.SaveChanges();

                // Store Sections
                var storeSections = new List<StoreSection>
                {
                    new() { Name = "Produce", Description = "Fresh fruits, vegetables, and herbs" },
                    new() { Name = "Dairy & Eggs", Description = "Milk, cheese, yogurt, and egg products" },
                    new() { Name = "Bakery", Description = "Fresh bread, pastries, and baked goods" },
                    new() { Name = "Meat & Seafood", Description = "Fresh and packaged meats, poultry, and fish" },
                    new() { Name = "Frozen Foods", Description = "Frozen meals, vegetables, and desserts" },
                    new() { Name = "Pantry Staples", Description = "Dry goods, grains, pasta, and cooking essentials" },
                    new() { Name = "Canned Goods", Description = "Preserved vegetables, soups, and prepared foods" },
                    new() { Name = "Snack Aisle", Description = "Chips, cookies, nuts, and convenience snacks" },
                    new() { Name = "Beverage Aisle", Description = "Non-alcoholic drinks, juices, and bottled waters" },
                    new() { Name = "Alcohol & Wine", Description = "Beer, wine, and spirits section" },
                    new() { Name = "Health & Wellness", Description = "Vitamins, supplements, and health foods" },
                    new() { Name = "Household Essentials", Description = "Cleaning supplies and paper goods" },
                    new() { Name = "International Cuisine", Description = "Ethnic foods and imported products" },
                    new() { Name = "Specialty Diets", Description = "Gluten-free, vegan, and allergy-friendly products" },
                    new() { Name = "Deli & Prepared Foods", Description = "Ready-to-eat meals and charcuterie" },
                    new() { Name = "Condiments & Sauces", Description = "Spices, oils, dressings, and flavorings" },
                    new() { Name = "Baby & Child Care", Description = "Infant formula and child nutrition products" },
                    new() { Name = "Pet Care", Description = "Pet food and animal supplies" },
                    new() { Name = "Seasonal & Holiday", Description = "Special occasion items and decorations" },
                    new() { Name = "Checkout Zone", Description = "Impulse buy items near registers" }
                };

                context.StoreSections.AddRange(storeSections);
                context.SaveChanges();

                var foodItems = new List<FoodItem>
                {
                    // Vegetables
                    new() {
                        Name = "Carrot", Description = "Orange root vegetable",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Vegetables").FoodCategoryId,
                        CaloriesPerUnit = 41, ProteinPerUnit = 0.9M
                    },
                    new() {
                        Name = "Spinach", Description = "Leafy green vegetable",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Vegetables").FoodCategoryId,
                        CaloriesPerUnit = 23, ProteinPerUnit = 2.9M
                    },
                    new() {
                        Name = "Bell Pepper", Description = "Colorful capsicum",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Vegetables").FoodCategoryId,
                        CaloriesPerUnit = 31, ProteinPerUnit = 1.0M
                    },

                    // Fruits
                    new() {
                        Name = "Apple", Description = "Crisp pomaceous fruit",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Fruits").FoodCategoryId,
                        CaloriesPerUnit = 52, ProteinPerUnit = 0.3M
                    },
                    new() {
                        Name = "Banana", Description = "Tropical curved fruit",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Fruits").FoodCategoryId,
                        CaloriesPerUnit = 89, ProteinPerUnit = 1.1M
                    },
                    new() {
                        Name = "Grapes", Description = "Small sweet berries",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Fruits").FoodCategoryId,
                        CaloriesPerUnit = 69, ProteinPerUnit = 0.7M
                    },

                    // Dairy & Alternatives
                    new() {
                        Name = "Whole Milk", Description = "Dairy milk 3.25% fat",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Dairy & Alternatives").FoodCategoryId,
                        CaloriesPerUnit = 61, ProteinPerUnit = 3.2M
                    },
                    new() {
                        Name = "Almond Milk", Description = "Plant-based milk alternative",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Dairy & Alternatives").FoodCategoryId,
                        CaloriesPerUnit = 39, ProteinPerUnit = 1.0M
                    },
                    new() {
                        Name = "Greek Yogurt", Description = "Strained yogurt high in protein",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Dairy & Alternatives").FoodCategoryId,
                        CaloriesPerUnit = 59, ProteinPerUnit = 10M
                    },

                    // Grains & Cereals
                    new() {
                        Name = "Brown Rice", Description = "Whole grain rice",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Grains & Cereals").FoodCategoryId,
                        CaloriesPerUnit = 111, ProteinPerUnit = 2.6M
                    },
                    new() {
                        Name = "Whole Wheat Bread", Description = "High fiber bread",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Grains & Cereals").FoodCategoryId,
                        CaloriesPerUnit = 247, ProteinPerUnit = 13M
                    },
                    new() {
                        Name = "Steel-cut Oats", Description = "Minimally processed oats",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Grains & Cereals").FoodCategoryId,
                        CaloriesPerUnit = 150, ProteinPerUnit = 5M
                    },

                    // Proteins
                    new() {
                        Name = "Chicken Breast", Description = "Boneless skinless poultry",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Proteins").FoodCategoryId,
                        CaloriesPerUnit = 165, ProteinPerUnit = 31M
                    },
                    new() {
                        Name = "Black Beans", Description = "Plant-based protein source",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Proteins").FoodCategoryId,
                        CaloriesPerUnit = 132, ProteinPerUnit = 8.9M
                    },
                    new() {
                        Name = "Salmon Fillet", Description = "Fatty fish protein",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Proteins").FoodCategoryId,
                        CaloriesPerUnit = 208, ProteinPerUnit = 20M
                    },

                    // Seafood
                    new() {
                        Name = "Shrimp", Description = "Small saltwater crustacean",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Seafood").FoodCategoryId,
                        CaloriesPerUnit = 99, ProteinPerUnit = 24M
                    },
                    new() {
                        Name = "Tuna Steak", Description = "Meaty saltwater fish",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Seafood").FoodCategoryId,
                        CaloriesPerUnit = 132, ProteinPerUnit = 28M
                    },

                    // Baked Goods
                    new() {
                        Name = "Sourdough Bread", Description = "Fermented dough bread",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Baked Goods").FoodCategoryId,
                        CaloriesPerUnit = 289, ProteinPerUnit = 9M
                    },
                    new() {
                        Name = "Croissant", Description = "Flaky French pastry",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Baked Goods").FoodCategoryId,
                        CaloriesPerUnit = 406, ProteinPerUnit = 8M
                    },

                    // Condiments & Sauces
                    new() {
                        Name = "Olive Oil", Description = "Mediterranean cooking oil",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Fats & Oils").FoodCategoryId,
                        CaloriesPerUnit = 884, ProteinPerUnit = 0M
                    },
    
                    // Special Diets
                    new() {
                        Name = "Gluten-Free Pasta", Description = "Wheat-free alternative",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Special Diets").FoodCategoryId,
                        CaloriesPerUnit = 357, ProteinPerUnit = 7.5M
                    },
    
                    // International
                    new() {
                        Name = "Sushi Rice", Description = "Japanese short-grain rice",
                        FoodCategoryId = foodCategories.First(c => c.Name == "International").FoodCategoryId,
                        CaloriesPerUnit = 130, ProteinPerUnit = 2.7M
                    },

                    // Frozen Foods
                    new() {
                        Name = "Frozen Peas", Description = "Flash-frozen vegetables",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Frozen Foods").FoodCategoryId,
                        CaloriesPerUnit = 81, ProteinPerUnit = 5.4M
                    },

                    // Legumes
                    new() {
                        Name = "Lentils", Description = "Nutritious pulses",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Legumes").FoodCategoryId,
                        CaloriesPerUnit = 116, ProteinPerUnit = 9M
                    },

                    // Nuts & Seeds
                    new() {
                        Name = "Almonds", Description = "Whole raw almonds",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Nuts & Seeds").FoodCategoryId,
                        CaloriesPerUnit = 579, ProteinPerUnit = 21M
                    },
    
                    // Herbs & Spices
                    new() {
                        Name = "Garlic", Description = "Fresh garlic cloves",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Herbs & Spices").FoodCategoryId,
                        CaloriesPerUnit = 149, ProteinPerUnit = 6.4M
                    },
    
                    // Grains & Cereals
                    new() {
                        Name = "Whole Wheat Tortillas", Description = "Soft taco shells",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Grains & Cereals").FoodCategoryId,
                        CaloriesPerUnit = 138, ProteinPerUnit = 4M
                    },
    
                    // Vegetables
                    new() {
                        Name = "Onion", Description = "Yellow cooking onion",
                        FoodCategoryId = foodCategories.First(c => c.Name == "Vegetables").FoodCategoryId,
                        CaloriesPerUnit = 40, ProteinPerUnit = 1.1M
                    }
                };

                context.FoodItems.AddRange(foodItems);
                context.SaveChanges();

                // Food Item Store Sections
                var foodItemSections = new List<FoodItemStoreSection>
                {
                    // Vegetables
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Carrot").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 3
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Spinach").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 2
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Bell Pepper").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 4
                    },

                    // Fruits
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Apple").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 1
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Banana").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 1
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Grapes").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Produce").StoreSectionId,
                        ShelfNumber = 2
                    },

                    // Dairy & Alternatives
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Whole Milk").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Dairy & Eggs").StoreSectionId,
                        ShelfNumber = 2
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Almond Milk").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Dairy & Eggs").StoreSectionId,
                        ShelfNumber = 3
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Greek Yogurt").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Dairy & Eggs").StoreSectionId,
                        ShelfNumber = 1
                    },

                    // Grains & Cereals
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Brown Rice").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Pantry Staples").StoreSectionId,
                        ShelfNumber = 4
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Whole Wheat Bread").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Bakery").StoreSectionId,
                        ShelfNumber = 1
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Steel-cut Oats").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Pantry Staples").StoreSectionId,
                        ShelfNumber = 5
                    },

                    // Proteins
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Chicken Breast").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Meat & Seafood").StoreSectionId,
                        ShelfNumber = 5
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Black Beans").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Pantry Staples").StoreSectionId,
                        ShelfNumber = 6
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Salmon Fillet").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Meat & Seafood").StoreSectionId,
                        ShelfNumber = 7
                    },

                    // Seafood
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Shrimp").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Meat & Seafood").StoreSectionId,
                        ShelfNumber = 8
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Tuna Steak").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Meat & Seafood").StoreSectionId,
                        ShelfNumber = 7
                    },

                    // Baked Goods
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Sourdough Bread").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Bakery").StoreSectionId,
                        ShelfNumber = 2
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Croissant").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Bakery").StoreSectionId,
                        ShelfNumber = 3
                    },

                    // Special Diets
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Gluten-Free Pasta").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Specialty Diets").StoreSectionId,
                        ShelfNumber = 4
                    },

                    // International
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Sushi Rice").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "International Cuisine").StoreSectionId,
                        ShelfNumber = 9
                    },

                    // Frozen Foods
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Frozen Peas").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Frozen Foods").StoreSectionId,
                        ShelfNumber = 12
                    },

                    // Legumes
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Lentils").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Pantry Staples").StoreSectionId,
                        ShelfNumber = 7
                    },

                    // Fats & Oils
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Olive Oil").FoodItemId,
                        StoreSectionId = storeSections.First(s => s.Name == "Condiments & Sauces").StoreSectionId,
                        ShelfNumber = 5
                    }
                };

                context.FoodItemStoreSections.AddRange(foodItemSections);
                context.SaveChanges();

                // Price Histories
                var priceHistories = new List<PriceHistory>
                {
                    // Vegetables
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Carrot").FoodItemId,
                        Price = 1.29M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 6, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Carrot").FoodItemId,
                        Price = 1.49M,
                        StartDate = new DateTime(2023, 7, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Spinach").FoodItemId,
                        Price = 2.49M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 8, 31)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Spinach").FoodItemId,
                        Price = 1.99M,
                        StartDate = new DateTime(2023, 9, 1)
                    },

                    // Fruits
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Apple").FoodItemId,
                        Price = 1.29M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 9, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Apple").FoodItemId,
                        Price = 1.49M,
                        StartDate = new DateTime(2023, 10, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Banana").FoodItemId,
                        Price = 0.49M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Dairy & Alternatives
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Whole Milk").FoodItemId,
                        Price = 3.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 6, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Whole Milk").FoodItemId,
                        Price = 4.29M,
                        StartDate = new DateTime(2023, 7, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Almond Milk").FoodItemId,
                        Price = 3.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Grains & Cereals
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Brown Rice").FoodItemId,
                        Price = 2.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 5, 31)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Brown Rice").FoodItemId,
                        Price = 3.29M,
                        StartDate = new DateTime(2023, 6, 1)
                    },

                    // Proteins
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Chicken Breast").FoodItemId,
                        Price = 5.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 8, 31)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Chicken Breast").FoodItemId,
                        Price = 6.49M,
                        StartDate = new DateTime(2023, 9, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Salmon Fillet").FoodItemId,
                        Price = 9.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 4, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Salmon Fillet").FoodItemId,
                        Price = 8.99M,
                        StartDate = new DateTime(2023, 5, 1)
                    },

                    // Seafood
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Shrimp").FoodItemId,
                        Price = 12.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 3, 31)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Shrimp").FoodItemId,
                        Price = 14.99M,
                        StartDate = new DateTime(2023, 4, 1)
                    },

                    // Baked Goods
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Sourdough Bread").FoodItemId,
                        Price = 5.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Croissant").FoodItemId,
                        Price = 2.50M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Specialty Items
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Gluten-Free Pasta").FoodItemId,
                        Price = 4.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 11, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Gluten-Free Pasta").FoodItemId,
                        Price = 3.99M,
                        StartDate = new DateTime(2023, 12, 1)
                    },

                    // International
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Sushi Rice").FoodItemId,
                        Price = 6.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Frozen Foods
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Frozen Peas").FoodItemId,
                        Price = 2.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Legumes
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Lentils").FoodItemId,
                        Price = 1.99M,
                        StartDate = new DateTime(2023, 1, 1)
                    },

                    // Fats & Oils
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Olive Oil").FoodItemId,
                        Price = 8.99M,
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 9, 30)
                    },
                    new() {
                        FoodItemId = foodItems.First(f => f.Name == "Olive Oil").FoodItemId,
                        Price = 12.99M,
                        StartDate = new DateTime(2023, 10, 1)
                    }
                };

                context.PriceHistories.AddRange(priceHistories);
                context.SaveChanges();

                // Recipes
                var recipes = new List<Recipe>
                {
                    new() {
                        Title = "Chicken & Veggie Stir-Fry",
                        Description = "Quick Asian-inspired stir-fry with fresh vegetables",
                        PrepTimeMinutes = 20,
                        CookTimeMinutes = 15,
                        Servings = 4
                    },
                    new() {
                        Title = "Salmon Rice Bowl",
                        Description = "Healthy grain bowl with grilled salmon",
                        PrepTimeMinutes = 15,
                        CookTimeMinutes = 25,
                        Servings = 2
                    },
                    new() {
                        Title = "Greek Yogurt Parfait",
                        Description = "Layered breakfast parfait with fresh fruit",
                        PrepTimeMinutes = 5,
                        CookTimeMinutes = 0,
                        Servings = 1
                    },
                    new() {
                        Title = "Black Bean Tacos",
                        Description = "Vegetarian tacos with spicy black beans",
                        PrepTimeMinutes = 10,
                        CookTimeMinutes = 15,
                        Servings = 4
                    },
                    new() {
                        Title = "Shrimp Garlic Pasta",
                        Description = "Creamy pasta with fresh shrimp",
                        PrepTimeMinutes = 15,
                        CookTimeMinutes = 20,
                        Servings = 3
                    },
                    new() {
                        Title = "Lentil Soup",
                        Description = "Hearty vegetarian soup",
                        PrepTimeMinutes = 10,
                        CookTimeMinutes = 40,
                        Servings = 6
                    }
                };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Ingredients
                var ingredients = new List<Ingredient>
                {
                    // Chicken & Veggie Stir-Fry
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Chicken Breast").FoodItemId,
                        Quantity = 500,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Bell Pepper").FoodItemId,
                        Quantity = 2,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Carrot").FoodItemId,
                        Quantity = 200,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Olive Oil").FoodItemId,
                        Quantity = 30,
                        Unit = UnitType.Milliliter
                    },

                    // Salmon Rice Bowl
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Salmon Fillet").FoodItemId,
                        Quantity = 400,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Brown Rice").FoodItemId,
                        Quantity = 200,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Spinach").FoodItemId,
                        Quantity = 100,
                        Unit = UnitType.Gram
                    },

                    // Greek Yogurt Parfait
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Greek Yogurt Parfait").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Greek Yogurt").FoodItemId,
                        Quantity = 150,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Greek Yogurt Parfait").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Banana").FoodItemId,
                        Quantity = 1,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Greek Yogurt Parfait").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Almonds").FoodItemId,
                        Quantity = 30,
                        Unit = UnitType.Gram
                    },

                    // Black Bean Tacos
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Black Bean Tacos").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Black Beans").FoodItemId,
                        Quantity = 400,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Black Bean Tacos").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Bell Pepper").FoodItemId,
                        Quantity = 1,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Black Bean Tacos").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Whole Wheat Tortillas").FoodItemId, // Added
                        Quantity = 8,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Black Bean Tacos").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Onion").FoodItemId, // Added
                        Quantity = 1,
                        Unit = UnitType.Piece
                    },

                    // Shrimp Garlic Pasta
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Shrimp Garlic Pasta").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Shrimp").FoodItemId,
                        Quantity = 300,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Shrimp Garlic Pasta").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Gluten-Free Pasta").FoodItemId,
                        Quantity = 200,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Shrimp Garlic Pasta").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Garlic").FoodItemId,
                        Quantity = 3,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Shrimp Garlic Pasta").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Olive Oil").FoodItemId,
                        Quantity = 30,
                        Unit = UnitType.Milliliter
                    },

                    // Lentil Soup
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Lentil Soup").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Lentils").FoodItemId,
                        Quantity = 250,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Lentil Soup").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Carrot").FoodItemId,
                        Quantity = 150,
                        Unit = UnitType.Gram
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Lentil Soup").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Onion").FoodItemId,
                        Quantity = 1,
                        Unit = UnitType.Piece
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Lentil Soup").RecipeId,
                        FoodItemId = foodItems.First(fi => fi.Name == "Garlic").FoodItemId,
                        Quantity = 2,
                        Unit = UnitType.Piece
                    }

                    // Continue this pattern for all recipes...
                };
                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();

                // Recipe Steps
                var recipeSteps = new List<RecipeStep>
                {
                    // Chicken & Veggie Stir-Fry
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        StepNumber = 1,
                        Instruction = "Slice chicken breast into thin strips"
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        StepNumber = 2,
                        Instruction = "Julienne carrots and bell peppers"
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        StepNumber = 3,
                        Instruction = "Heat olive oil in wok and cook chicken until browned"
                    },

                    // Salmon Rice Bowl
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        StepNumber = 1,
                        Instruction = "Season salmon with salt and pepper"
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        StepNumber = 2,
                        Instruction = "Grill salmon at 200°C for 12-15 minutes"
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        StepNumber = 3,
                        Instruction = "Assemble bowl with rice, spinach, and flaked salmon"
                    },

                    // Continue detailed steps for all recipes...
                };
                context.RecipeSteps.AddRange(recipeSteps);
                context.SaveChanges();

                // Meal Suggestions
                var mealSuggestions = new List<MealSuggestion>
                {
                    new() {
                        Name = "High-Protein Lunch Pack",
                        MealType = MealType.Lunch,
                        Description = "Muscle-building meals with 30g+ protein",
                        EffectiveDate = new DateTime(2023, 1, 1),
                        ExpirationDate = new DateTime(2024, 1, 1)
                    },
                    new() {
                        Name = "Vegetarian Dinner Options",
                        MealType = MealType.Dinner,
                        Description = "Meat-free evening meals",
                        EffectiveDate = new DateTime(2023, 1, 1)
                    },
                    new() {
                        Name = "Quick Weeknight Dinners",
                        MealType = MealType.Dinner,
                        Description = "30-minute meals for busy families",
                        EffectiveDate = new DateTime(2023, 1, 1)
                    },
                    new() {
                        Name = "Mediterranean Diet Plan",
                        MealType = MealType.Dinner,
                        Description = "Heart-healthy Mediterranean recipes",
                        EffectiveDate = new DateTime(2023, 1, 1)
                    },
                    new() {
                        Name = "Post-Workout Recovery",
                        MealType = MealType.Snack,
                        Description = "Protein-packed recovery snacks",
                        EffectiveDate = new DateTime(2023, 1, 1)
                    },
                    new() {
                        Name = "Summer Salad Bar",
                        MealType = MealType.Lunch,
                        Description = "Light seasonal salads",
                        EffectiveDate = new DateTime(2023, 6, 1),
                        ExpirationDate = new DateTime(2023, 9, 30)
                    }
                };
                context.MealSuggestions.AddRange(mealSuggestions);
                context.SaveChanges();

                // Meal Suggestion Tags
                var tags = new List<MealSuggestionTag>
                {
                    new() { TagName = "Quick" },
                    new() { TagName = "Vegetarian" },
                    new() { TagName = "High-Protein" },
                    new() { TagName = "Gluten-Free" },
                    new() { TagName = "Low-Carb" },
                    new() { TagName = "Meal Prep" },
                    new() { TagName = "Budget-Friendly" },
                    new() { TagName = "Dairy-Free" },
                    new() { TagName = "Kid-Friendly" }
                };
                context.MealSuggestionTags.AddRange(tags);
                context.SaveChanges();

                // Recipe-Meal Suggestion Relationships
                var recipeMealSuggestions = new List<RecipeMealSuggestion>
                {
                    // High-Protein Lunch Pack
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "High-Protein Lunch Pack").MealSuggestionId
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "High-Protein Lunch Pack").MealSuggestionId
                    },
    
                    // Vegetarian Dinner Options
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Black Bean Tacos").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Vegetarian Dinner Options").MealSuggestionId
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Lentil Soup").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Vegetarian Dinner Options").MealSuggestionId
                    },
    
                    // Quick Weeknight Dinners
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Shrimp Garlic Pasta").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Quick Weeknight Dinners").MealSuggestionId
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Chicken & Veggie Stir-Fry").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Quick Weeknight Dinners").MealSuggestionId
                    },
    
                    // Mediterranean Diet Plan
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Salmon Rice Bowl").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Mediterranean Diet Plan").MealSuggestionId
                    },
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Greek Yogurt Parfait").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Mediterranean Diet Plan").MealSuggestionId
                    },
    
                    // Post-Workout Recovery
                    new() {
                        RecipeId = recipes.First(r => r.Title == "Greek Yogurt Parfait").RecipeId,
                        MealSuggestionId = mealSuggestions.First(m => m.Name == "Post-Workout Recovery").MealSuggestionId
                    },
                };

                context.RecipeMealSuggestions.AddRange(recipeMealSuggestions);
                context.SaveChanges();

                Console.WriteLine("Test data populated successfully!");
            }
        }
    }
}
