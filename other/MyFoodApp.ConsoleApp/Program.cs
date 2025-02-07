using Microsoft.EntityFrameworkCore;
using MyFoodApp.ConsoleApp.Utility;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace MyFoodApp.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            string projectRoot = GetProjectRoot();
            string jsonFilePath = Path.Combine(projectRoot, "Data\\seed_data.json");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyFoodApp;Trusted_Connection=True;")
                .Options;

            using (var context = new AppDbContext(options))
            {
                //ResetDatabase(context);

                // 1. Load data from JSON
                //var seedData = LoadSeedDataFromJson(jsonFilePath);

                // 2. Seed the database
                //SeedDatabase(seedData, context);

                // 3. Save data to JSON
                //SaveSeedDataToJson(jsonFilePath, GenerateSeedDataFromDatabase(context));
            }
        }

        public static void ResetDatabase(AppDbContext context)
        {
            // Drop the database if it exists
            context.Database.EnsureDeleted();

            // Recreate the database
            context.Database.Migrate();

            Console.WriteLine("Database has been reset successfully!");
        }

        // 1. Load data from JSON
        public static SeedData LoadSeedDataFromJson(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<SeedData>(json);
            }
        }

        // 2. Seed the database
        public static void SeedDatabase(SeedData seedData, AppDbContext context)
        {
            context.Database.EnsureCreated();
            
            // 1. Add Food Categories
            context.FoodCategories.AddRange(seedData.FoodCategories);
            context.SaveChanges();

            // 2. Add Store Sections
            context.StoreSections.AddRange(seedData.StoreSections);
            context.SaveChanges();

            // 3. Add Food Items
            context.FoodItems.AddRange(seedData.FoodItems);
            context.SaveChanges();

            // 4. Add Food Item Store Sections
            context.FoodItemStoreSections.AddRange(seedData.FoodItemStoreSections);
            context.SaveChanges();

            // 5. Add Price Histories
            context.PriceHistories.AddRange(seedData.PriceHistories);
            context.SaveChanges();

            // 6. Add Meal Suggestions
            context.MealSuggestions.AddRange(seedData.MealSuggestions);
            context.SaveChanges();

            // 7. Add Meal Suggestion Tags
            context.MealSuggestionTags.AddRange(seedData.MealSuggestionTags);
            context.SaveChanges();

            Console.WriteLine("Test data populated successfully!");
        }

        // 3. Save data to JSON
        public static void SaveSeedDataToJson(string filePath, SeedData seedData)
        {
            Console.WriteLine("SaveSeedDataToJson - Start");
            using (StreamWriter w = new StreamWriter(filePath))
            {
                string json = JsonConvert.SerializeObject(seedData, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                w.Write(json);
            }
            Console.WriteLine("SaveSeedDataToJson - End");
        }

        // 4. Save Existing Database to JSON
        public static SeedData GenerateSeedDataFromDatabase(AppDbContext context)
        {
            var seedData = new SeedData();

            Console.WriteLine("// 1. Load Food Categories");
            seedData.FoodCategories = context.FoodCategories.ToList();

            Console.WriteLine("// 2. Load Store Sections");
            seedData.StoreSections = context.StoreSections.ToList();

            Console.WriteLine("// 3. Load Food Items");
            seedData.FoodItems = context.FoodItems
                .OrderBy(x => x.FoodCategoryId)
                .ThenBy(x => x.Name)
                .GroupBy(x => new { x.FoodCategoryId, x.Name })
                .Select(g => g.First())
                .ToList();

            Console.WriteLine("// 4. Load FoodItemStoreSections");
            seedData.FoodItemStoreSections = context.FoodItemStoreSections.ToList();

            Console.WriteLine("// 5. Load Price Histories");
            seedData.PriceHistories = context.PriceHistories.ToList();

            Console.WriteLine("// 6. Load Meal Suggestions");
            seedData.MealSuggestions = context.MealSuggestions.ToList();

            Console.WriteLine("// 7. Load Meal Suggestion Tags");
            seedData.MealSuggestionTags = context.MealSuggestionTags.ToList();

            return seedData;
        }

        private static string GetProjectRoot()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectRoot = FindProjectRoot(currentDirectory, "MyFoodApp.ConsoleApp.csproj");

            if (projectRoot == null)
            {
                throw new Exception("Could not find project root.");
            }
            return projectRoot;
        }

        private static string FindProjectRoot(string currentDir, string projectFileName)
        {
            string dir = currentDir;
            while (!File.Exists(Path.Combine(dir, projectFileName)))
            {
                dir = Directory.GetParent(dir)?.FullName;
                if (dir == null) return null;
            }
            return dir;
        }
    }
}
