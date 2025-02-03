using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces.Recipes;
using MyFoodApp.Application.Tests.Data;
using MyFoodApp.Application.UseCases.Recipes;
using MyFoodApp.Domain.Enums;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Tests.Data;
using Xunit;

namespace MyFoodApp.Application.Tests.UseCases.Foods
{
    [Collection("ApplicationTestCollection")]
    public class RecipeUseCasesTests
    {
        private readonly AppDbContext _context;
        private readonly IRecipeUseCases _recipeUseCases;
        private readonly ILogger<RecipeUseCases> _logger;

        public RecipeUseCasesTests(ApplicationTestFixture fixture)
        {
            _context = fixture.DbContext;
            _logger = new LoggerFactory().CreateLogger<RecipeUseCases>();
            _recipeUseCases = new RecipeUseCases(fixture.RecipeRepository, fixture.Mapper, _logger);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipeDto_WhenRecipeIsCreated()
        {
            // Arrange
            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto();

            // Act
            var result = await _recipeUseCases.CreateRecipeAsync(recipeDto);

            // Assert
            using (var scope = new FluentAssertions.Execution.AssertionScope())
            {
                result.Should().NotBeNull();
                result.Item.Should().BeEquivalentTo(recipeDto, opt => opt.ExcludingMissingMembers());
                result.List.Should().BeEmpty();
                result.TotalItems.Should().Be(1);
                result.ErrorList.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnUpdatedRecipeDto_WhenUpdateIsSuccessful()
        {
            // Arrange
            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto();
            var createdRecipe = await _recipeUseCases.CreateRecipeAsync(recipeDto);

            var updatedRecipeDto = new RecipeDto
            {
                RecipeId = createdRecipe.Item.RecipeId,
                Title = "Updated Recipe",
                Description = "Updated Description",
                Ingredients = createdRecipe.Item.Ingredients,
                Steps = createdRecipe.Item.Steps,
                MealSuggestions = createdRecipe.Item.MealSuggestions
            };

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(createdRecipe.Item.RecipeId, updatedRecipeDto);

            // Assert
            using (var scope = new FluentAssertions.Execution.AssertionScope())
            {
                result.Should().NotBeNull();
                result.Item.Should().BeEquivalentTo(updatedRecipeDto, opt => opt.ExcludingMissingMembers());
                result.List.Should().BeEmpty();
                result.TotalItems.Should().Be(1);
                result.ErrorList.Should().BeEmpty();
            }
        }
    }
}
