using Bogus;
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Infrastructure.Tests.Data
{
    public static class TestDataFactory
    {
        public static FoodItem CreateFoodItem(int categoryId)
        {
            return new Faker<FoodItem>()
                .RuleFor(fi => fi.FoodItemId, f => f.IndexGlobal + 1)
                .RuleFor(f => f.Name, f => f.Commerce.ProductName())
                .RuleFor(f => f.Description, f => f.Commerce.ProductDescription())
                .RuleFor(f => f.FoodCategoryId, categoryId)
                .Generate();
        }

        // Food Category
        public static FoodCategory CreateFoodCategory(int? id = null)
        {
            return new Faker<FoodCategory>()
                .RuleFor(fc => fc.FoodCategoryId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(fc => fc.Name, f => f.Commerce.Department())
                .RuleFor(fc => fc.Description, f => f.Lorem.Sentence())
                .Generate();
        }

        // Food Items (single and multiple)
        public static FoodItem CreateFoodItem(int categoryId, int? id = null)
        {
            return new Faker<FoodItem>()
                .RuleFor(fi => fi.FoodItemId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(fi => fi.Name, f => f.Commerce.ProductName())
                .RuleFor(fi => fi.Description, f => f.Commerce.ProductDescription())
                .RuleFor(fi => fi.FoodCategoryId, categoryId)
                .RuleFor(fi => fi.CaloriesPerUnit, f => f.Random.Decimal(50, 500))
                .RuleFor(fi => fi.ProteinPerUnit, f => f.Random.Decimal(0, 50))
                .Generate();
        }

        public static List<FoodItem> CreateFoodItems(int count, int categoryId)
        {
            return Enumerable.Range(1, count)
                .Select(i => CreateFoodItem(categoryId))
                .ToList();
        }

        // Price History
        public static PriceHistory CreatePriceHistory(int foodItemId, int? id = null)
        {
            return new Faker<PriceHistory>()
                .RuleFor(ph => ph.PriceHistoryId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(ph => ph.FoodItemId, foodItemId)
                .RuleFor(ph => ph.Price, f => f.Finance.Amount(1, 100))
                .RuleFor(ph => ph.StartDate, f => f.Date.Recent())
                .Generate();
        }

        // Store Section
        public static StoreSection CreateStoreSection(int? id = null)
        {
            return new Faker<StoreSection>()
                .RuleFor(ss => ss.StoreSectionId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(ss => ss.Name, f => f.Commerce.Department())
                .RuleFor(ss => ss.Description, f => f.Lorem.Sentence())
                .Generate();
        }

        // FoodItemStoreSection Relationship
        public static FoodItemStoreSection CreateFoodItemStoreSection(int foodItemId, int storeSectionId)
        {
            return new FoodItemStoreSection
            {
                FoodItemId = foodItemId,
                StoreSectionId = storeSectionId
            };
        }
    }
}
