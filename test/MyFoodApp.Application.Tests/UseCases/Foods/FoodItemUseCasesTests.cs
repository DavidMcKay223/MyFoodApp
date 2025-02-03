using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces.Recipes;
using MyFoodApp.Application.UseCases.Recipes;
using MyFoodApp.Domain.Enums;
using Xunit;

namespace MyFoodApp.Application.Tests.UseCases.Foods
{
    [Collection("ApplicationTestCollection")]
    public class RecipeUseCasesTests
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeUseCases> _logger;
        private readonly IRecipeUseCases _recipeUseCases;

        public RecipeUseCasesTests(ApplicationTestFixture fixture)
        {
            _mapper = fixture.Mapper;
            _logger = new LoggerFactory().CreateLogger<RecipeUseCases>();
            _recipeUseCases = new RecipeUseCases(
                fixture.RecipeRepository,
                _mapper,
                _logger
            );
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipeDto_WhenRecipeIsCreated()
        {
            // Arrange
            var recipeDto = new RecipeDto
            {
                Title = "Test Recipe",
                Description = "Test Description",
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { Quantity = 1, Unit = UnitType.Gram, FoodItemId = 1 }
                },
                Steps = new List<RecipeStepDto>
                {
                    new RecipeStepDto { StepNumber = 1, Instruction = "Test Step" }
                },
                MealSuggestions = new List<RecipeMealSuggestionDto>
                {
                    new RecipeMealSuggestionDto { MealSuggestionId = 1 }
                }
            };

            // Act
            var result = await _recipeUseCases.CreateRecipeAsync(recipeDto);

            // Assert
            using (var scope = new FluentAssertions.Execution.AssertionScope())
            {
                result.Should().NotBeNull();
                result.Item.Should().BeEquivalentTo(recipeDto, opt => opt.ExcludingMissingMembers());
                result.List.Should().BeEmpty();
                result.TotalItems.Should().Be(0);
                result.ErrorList.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnUpdatedRecipeDto_WhenUpdateIsSuccessful()
        {
            // Arrange
            var recipeId = 1;
            var existingRecipeDto = new RecipeDto
            {
                RecipeId = recipeId,
                Title = "Original Recipe",
                Description = "Test Description",
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { IngredientId = 1, Quantity = 1, Unit = UnitType.Gram, FoodItemId = 1 }
                },
                Steps = new List<RecipeStepDto>
                {
                    new RecipeStepDto { StepId = 1, StepNumber = 1, Instruction = "Original Step" }
                },
                MealSuggestions = new List<RecipeMealSuggestionDto>
                {
                    new RecipeMealSuggestionDto { MealSuggestionId = 1 }
                }
            };

            await _recipeUseCases.CreateRecipeAsync(existingRecipeDto);

            var updatedRecipeDto = new RecipeDto
            {
                RecipeId = recipeId,
                Title = "Updated Recipe",
                Description = "Test Description",
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { IngredientId = 1, Quantity = 2, Unit = UnitType.Kilogram, FoodItemId = 1 }
                },
                Steps = new List<RecipeStepDto>
                {
                    new RecipeStepDto { StepId = 1, StepNumber = 1, Instruction = "Updated Step" }
                },
                MealSuggestions = new List<RecipeMealSuggestionDto>
                {
                    new RecipeMealSuggestionDto { MealSuggestionId = 1 }
                }
            };

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(recipeId, updatedRecipeDto);

            // Assert
            using (var scope = new FluentAssertions.Execution.AssertionScope())
            {
                result.Should().NotBeNull();
                result.Item.Should().BeEquivalentTo(updatedRecipeDto, opt => opt.ExcludingMissingMembers());
                result.List.Should().BeEmpty();
                result.TotalItems.Should().Be(0);
                result.ErrorList.Should().BeEmpty();
            }
        }
    }
}
