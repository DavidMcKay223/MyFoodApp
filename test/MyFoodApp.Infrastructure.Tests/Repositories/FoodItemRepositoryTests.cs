using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Tests.Helpers;
using MyFoodApp.Infrastructure.Tests.Data;

namespace MyFoodApp.Infrastructure.Tests.Repositories
{
    public class FoodItemRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IFoodItemRepository _repository;

        public FoodItemRepositoryTests()
        {
            _context = DbContextHelper.CreateTestContext();
            _repository = new FoodItemRepository(_context);
        }

        [Fact]
        public async Task GetFoodItemByIdAsync_ReturnsItem_WhenExists()
        {
            // Arrange
            var testItem = DomainTestDataFactory.CreateFoodItem(1);
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory(1));
                ctx.FoodItems.Add(testItem);
            });

            // Act
            var result = await _repository.GetFoodItemByIdAsync(testItem.FoodItemId);

            // Assert
            result.Should().NotBeNull();
            result!.FoodItemId.Should().Be(testItem.FoodItemId);
            result.Name.Should().NotBeNullOrEmpty();
            result.Description.Should().NotBeNullOrEmpty();
            result.FoodCategoryId.Should().Be(1);
        }

        [Fact]
        public async Task GetAllFoodItemsAsync_ReturnsAllItems()
        {
            // Arrange
            var testItems = DomainTestDataFactory.CreateFoodItems(3, 1); 
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory(1));
                ctx.FoodItems.AddRange(testItems);
            });

            // Act
            var result = await _repository.GetAllFoodItemsAsync().ToListAsync();

            // Assert
            result.Should().HaveCount(3);
            result.Should().AllSatisfy(item =>
            {
                item.Name.Should().NotBeNullOrEmpty();
                item.Description.Should().NotBeNullOrEmpty();
                item.FoodCategoryId.Should().Be(1);
            });
        }

        public void Dispose()
        {
            DbContextHelper.CleanDatabase(_context);
        }
    }
}
