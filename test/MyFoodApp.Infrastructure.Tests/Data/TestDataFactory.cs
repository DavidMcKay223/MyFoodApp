using Bogus;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Infrastructure.Tests.Data
{
    public static class TestDataFactory
    {
        public static FoodCategory CreateFoodCategory(int? id = null)
        {
            return new Faker<FoodCategory>()
                .RuleFor(fc => fc.FoodCategoryId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(fc => fc.Name, f => f.Commerce.Department())
                .RuleFor(fc => fc.Description, f => f.Lorem.Sentence())
                .Generate();
        }

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
            return new Faker<FoodItem>()
                .RuleFor(fi => fi.FoodItemId, f => f.IndexGlobal + 1)
                .RuleFor(fi => fi.Name, f => f.Commerce.ProductName())
                .RuleFor(fi => fi.Description, f => f.Commerce.ProductDescription())
                .RuleFor(fi => fi.FoodCategoryId, categoryId)
                .RuleFor(fi => fi.CaloriesPerUnit, f => f.Random.Decimal(50, 500))
                .RuleFor(fi => fi.ProteinPerUnit, f => f.Random.Decimal(0, 50))
                .Generate(count);
        }
    }
}
