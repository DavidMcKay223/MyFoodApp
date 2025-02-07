using Microsoft.EntityFrameworkCore;
using MyFoodApp.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.ConsoleApp.Utility
{
    public class DataSeeder
    {
        private readonly string _jsonFilePath;
        private readonly DbContextOptions<AppDbContext> _options;

        public DataSeeder(string jsonFilePath, DbContextOptions<AppDbContext> options)
        {
            _jsonFilePath = jsonFilePath;
            _options = options;
        }

        public void ResetDatabase()
        {
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                Console.WriteLine("Database has been reset successfully!");
            }
        }

        public void SeedDatabaseFromJson()
        {
            using (var context = new AppDbContext(_options))
            {
                var seedData = LoadSeedDataFromJson(_jsonFilePath);
                SeedDatabase(seedData, context);
                Console.WriteLine("Database has been seeded successfully!");
            }
        }

        public void SaveDatabaseToJson()
        {
            using (var context = new AppDbContext(_options))
            {
                SaveSeedDataToJson(_jsonFilePath, GenerateSeedDataFromDatabase(context));
                Console.WriteLine("Database has been saved to JSON successfully!");
            }
        }

        private SeedData LoadSeedDataFromJson(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<SeedData>(json);
            }
        }

        private void SeedDatabase(SeedData seedData, AppDbContext context)
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
        }

        private void SaveSeedDataToJson(string filePath, SeedData seedData)
        {
            string json = JsonConvert.SerializeObject(seedData, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private SeedData GenerateSeedDataFromDatabase(AppDbContext context)
        {
            var seedData = new SeedData();

            Console.WriteLine("Generating Seed Data from Database!");
            Console.WriteLine("1. Load Food Categories");
            seedData.FoodCategories = context.FoodCategories.ToList();

            Console.WriteLine("2. Load Store Sections");
            seedData.StoreSections = context.StoreSections.ToList();

            Console.WriteLine("3. Load Food Items");
            seedData.FoodItems = context.FoodItems
                .OrderBy(x => x.FoodCategoryId)
                .ThenBy(x => x.Name)
                .GroupBy(x => new { x.FoodCategoryId, x.Name })
                .Select(g => g.First())
                .ToList();

            Console.WriteLine("4. Load FoodItemStoreSections");
            seedData.FoodItemStoreSections = context.FoodItemStoreSections.ToList();

            Console.WriteLine("5. Load Price Histories");
            seedData.PriceHistories = context.PriceHistories.ToList();

            Console.WriteLine("6. Load Meal Suggestions");
            seedData.MealSuggestions = context.MealSuggestions.ToList();

            Console.WriteLine("7. Load Meal Suggestion Tags");
            seedData.MealSuggestionTags = context.MealSuggestionTags.ToList();

            return seedData;
        }
    }
}
