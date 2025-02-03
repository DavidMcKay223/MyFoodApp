using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces.Recipes;
using MyFoodApp.Application.Tests.Data;
using MyFoodApp.Application.UseCases.Recipes;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Enums;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Tests.Data;
using MyFoodApp.Infrastructure.Tests.Helpers;
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
        public async Task LookupRecipesAsync_ShouldReturnRecipes_WhenValidSearchDto()
        {
            // Arrange
            var searchDto = new RecipeSearchDto { Title = "Test Recipe" };
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.Recipes.Add(new Recipe() { Title = "Test Recipe", Description = ""});
            });

            // Act
            var result = await _recipeUseCases.LookupRecipesAsync(searchDto);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.List.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async Task SuggestRecipesBasedOnIngredientsAsync_ShouldReturnRecipes_WhenValidIngredientIds()
        {
            // Arrange
            var ingredientIds = new List<int> { 1, 2, 3 };

            // Act
            var result = await _recipeUseCases.SuggestRecipesBasedOnIngredientsAsync(ingredientIds);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.List.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async Task GetRecipeByIdAsync_ShouldReturnRecipe_WhenValidRecipeId()
        {
            // Arrange
            var recipeId = 1;
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
            });

            // Act
            var result = await _recipeUseCases.GetRecipeByIdAsync(recipeId);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.Item.Should().NotBeNull();
                result.Item!.RecipeId.Should().Be(recipeId);
            }
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto()
        {
            // Arrange
            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto(0);

            // Act
            var result = await _recipeUseCases.CreateRecipeAsync(recipeDto);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.Item.Should().NotBeNull();
                result.Item!.Title.Should().Be(recipeDto.Title);
            }
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
            });
            
            var recipeId = _context.Recipes.First().RecipeId;
            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto(recipeId);

            recipeDto.Steps.Add(ApplicationTestDataFactory.CreateRecipeStepDto(recipeId));
            recipeDto.MealSuggestions.Add(ApplicationTestDataFactory.CreateRecipeMealSuggestionDto(recipeId, 1));
            recipeDto.Ingredients.Add(ApplicationTestDataFactory.CreateIngredientDto(recipeId, 1));

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(recipeId, recipeDto);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.Item.Should().NotBeNull();
                result.Item.RecipeId.Should().Be(recipeId);
                result.Item.Title.Should().Be(recipeDto.Title);
            }
        }

        [Fact]
        public async Task DeleteRecipeAsync_ShouldReturnRecipe_WhenValidRecipeId()
        {
            // Arrange
            var recipeId = 1;
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
            });

            // Act
            var result = await _recipeUseCases.DeleteRecipeAsync(recipeId);

            // Assert
            using (new AssertionScope())
            {
                result.ErrorList.Should().BeEmpty();
                result.Should().NotBeNull();
                result.Item.Should().BeNull();
            }
        }
    }
}
