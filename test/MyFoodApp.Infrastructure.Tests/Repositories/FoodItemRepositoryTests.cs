using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Tests.Data;

namespace MyFoodApp.Infrastructure.Tests.Repositories
{
    public class FoodItemRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly FoodItemRepository _repository;
        private readonly FoodCategory _testCategory;

        public FoodItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            _context = new AppDbContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            // Create required FoodCategory first
            _testCategory = DomainTestDataFactory.CreateFoodCategory();
            _context.FoodCategories.Add(_testCategory);
            _context.SaveChanges();

            _repository = new FoodItemRepository(_context);
        }

        [Fact]
        public async Task GetFoodItemByIdAsync_ReturnsItem_WhenExists()
        {
            // Arrange
            var testItem = DomainTestDataFactory.CreateFoodItem(_testCategory.FoodCategoryId);
            _context.FoodItems.Add(testItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetFoodItemByIdAsync(testItem.FoodItemId);

            // Assert
            result.Should().NotBeNull();
            result!.FoodItemId.Should().Be(testItem.FoodItemId);
            result.Name.Should().NotBeNullOrEmpty();
            result.Description.Should().NotBeNullOrEmpty();
            result.FoodCategoryId.Should().Be(_testCategory.FoodCategoryId);
        }

        [Fact]
        public async Task GetAllFoodItemsAsync_ReturnsAllItems()
        {
            // Arrange
            var testItems = DomainTestDataFactory.CreateFoodItems(3, _testCategory.FoodCategoryId);
            _context.FoodItems.AddRange(testItems);
            await _context.SaveChangesAsync();

            // Act
            var result = _repository.GetAllFoodItemsAsync().ToList();

            // Assert
            result.Should().HaveCount(3);
            result.Should().AllSatisfy(item =>
            {
                item.Name.Should().NotBeNullOrEmpty();
                item.Description.Should().NotBeNullOrEmpty();
                item.FoodCategoryId.Should().Be(_testCategory.FoodCategoryId);
            });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}