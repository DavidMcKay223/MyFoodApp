using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Infrastructure.Tests.Repositories
{
    public class FoodItemRepositoryTests : SqliteTestBase
    {
        [Fact]
        public void TestWithSeededDataUsingFactory()
        {
            // Arrange
            var testCategory = TestDataFactory.CreateFoodCategory();
            var testItems = new[]
            {
                TestDataFactory.CreateFoodItem(testCategory.FoodCategoryId),
                TestDataFactory.CreateFoodItem(testCategory.FoodCategoryId),
                TestDataFactory.CreateFoodItem(testCategory.FoodCategoryId)
            };

            Seed(context =>
            {
                context.FoodCategories.Add(testCategory);
                context.FoodItems.AddRange(testItems);
            });

            // Act
            var itemsFromDb = _context.FoodItems
                .Include(f => f.FoodCategory)
                .ToList();

            // Assert
            itemsFromDb.Should().NotBeNull()
                .And.HaveCount(3)
                .And.AllSatisfy(item =>
                {
                    item.FoodCategory.Should().NotBeNull();
                    item.FoodCategory!.Name.Should().Be(testCategory.Name);
                    item.FoodCategoryId.Should().Be(testCategory.FoodCategoryId);
                });
        }

        [Fact]
        public void TestComplexRelationships()
        {
            // Arrange
            var category = TestDataFactory.CreateFoodCategory();
            var storeSection = TestDataFactory.CreateStoreSection();
            var foodItems = TestDataFactory.CreateFoodItems(3, category.FoodCategoryId);
            var priceHistory = TestDataFactory.CreatePriceHistory(foodItems[0].FoodItemId);

            Seed(context =>
            {
                context.FoodCategories.Add(category);
                context.StoreSections.Add(storeSection);
                context.FoodItems.AddRange(foodItems);
                context.PriceHistories.Add(priceHistory);
                context.FoodItemStoreSections.Add(
                    TestDataFactory.CreateFoodItemStoreSection(
                        foodItems[0].FoodItemId,
                        storeSection.StoreSectionId
                    )
                );
            });

            // Act
            var itemWithRelations = _context.FoodItems
                .Include(f => f.FoodCategory)
                .Include(f => f.PriceHistories)
                .Include(f => f.StoreSections)
                .ThenInclude(fss => fss.StoreSection)
                .First(f => f.FoodItemId == foodItems[0].FoodItemId);

            // Assert
            itemWithRelations.Should().NotBeNull();
            itemWithRelations.FoodCategory.Should().NotBeNull()
                .And.BeEquivalentTo(category, options =>
                    options.ExcludingMissingMembers());

            itemWithRelations.PriceHistories.Should()
                .ContainSingle()
                .Which.Should().BeEquivalentTo(priceHistory, options =>
                    options.ExcludingMissingMembers());

            itemWithRelations.StoreSections.Should()
                .ContainSingle()
                .Which.StoreSection.Should().BeEquivalentTo(storeSection, options =>
                    options.ExcludingMissingMembers());
        }
    }
}
