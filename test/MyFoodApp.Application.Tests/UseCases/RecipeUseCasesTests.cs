using Shouldly;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Application.Tests.Data;
using MyFoodApp.Application.UseCases;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Tests.Data;
using MyFoodApp.Infrastructure.Tests.Helpers;

namespace MyFoodApp.Application.Tests.UseCases
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
            var temRecipe = DomainTestDataFactory.CreateRecipe();
            temRecipe.Title = "Test Recipe";
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(temRecipe);
            });

            // Act
            var result = await _recipeUseCases.LookupRecipesAsync(searchDto);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task SuggestRecipesBasedOnIngredientsAsync_ShouldReturnRecipes_WhenValidIngredientIds()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory());
            });
            var foodCategoryId = _context.FoodCategories.First().FoodCategoryId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.FoodItems.Add(DomainTestDataFactory.CreateFoodItem(foodCategoryId));
            });
            var recipeId = _context.Recipes.First().RecipeId;
            var foodItemId = _context.FoodItems.First().FoodItemId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Ingredients.Add(DomainTestDataFactory.CreateIngredient(recipeId, foodItemId));
                ctx.Ingredients.Add(DomainTestDataFactory.CreateIngredient(recipeId, foodItemId));
                ctx.Ingredients.Add(DomainTestDataFactory.CreateIngredient(recipeId, foodItemId));
            });

            var ingredientIds = new List<int>() { foodItemId };

            // Act
            var result = await _recipeUseCases.SuggestRecipesBasedOnIngredientsAsync(ingredientIds);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetRecipeByIdAsync_ShouldReturnRecipe_WhenValidRecipeId()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
            });
            var recipeId = _context.Recipes.First().RecipeId;

            // Act
            var result = await _recipeUseCases.GetRecipeByIdAsync(recipeId);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldNotBeNull();
            result.Item.RecipeId.ShouldBe(recipeId);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory());
            });
            var foodCategoryId = _context.FoodCategories.First().FoodCategoryId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.FoodItems.Add(DomainTestDataFactory.CreateFoodItem(foodCategoryId));
                ctx.MealSuggestions.Add(DomainTestDataFactory.CreateMealSuggestion());
            });
            var recipeId = _context.Recipes.First().RecipeId;
            var foodItemId = _context.FoodItems.First().FoodItemId;
            var mealSuggestionId = _context.MealSuggestions.First().MealSuggestionId;

            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto();

            recipeDto.Steps.Add(ApplicationTestDataFactory.CreateRecipeStepDto(recipeId));
            recipeDto.MealSuggestions.Add(ApplicationTestDataFactory.CreateRecipeMealSuggestionDto(recipeId, mealSuggestionId));
            recipeDto.Ingredients.Add(ApplicationTestDataFactory.CreateIngredientDto(recipeId, foodItemId));

            // Act
            var result = await _recipeUseCases.CreateRecipeAsync(recipeDto);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldNotBeNull();
            result.Item.Title.ShouldBe(recipeDto.Title);
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto_NewRecords()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory());
            });
            var foodCategoryId = _context.FoodCategories.First().FoodCategoryId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.FoodItems.Add(DomainTestDataFactory.CreateFoodItem(foodCategoryId));
                ctx.MealSuggestions.Add(DomainTestDataFactory.CreateMealSuggestion());
            });
            var recipeId = _context.Recipes.First().RecipeId;
            var foodItemId = _context.FoodItems.First().FoodItemId;
            var mealSuggestionId = _context.MealSuggestions.First().MealSuggestionId;

            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto(recipeId);

            recipeDto.Steps.Add(ApplicationTestDataFactory.CreateRecipeStepDto(recipeId, 0));
            recipeDto.MealSuggestions.Add(ApplicationTestDataFactory.CreateRecipeMealSuggestionDto(recipeId, mealSuggestionId));
            recipeDto.Ingredients.Add(ApplicationTestDataFactory.CreateIngredientDto(recipeId, foodItemId, 0));

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(recipeId, recipeDto);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldNotBeNull();
            result.Item.RecipeId.ShouldBe(recipeId);
            result.Item.Title.ShouldBe(recipeDto.Title);
            result.Item.Steps.Count.ShouldBe(1);
            result.Item.MealSuggestions.Count.ShouldBe(1);
            result.Item.Ingredients.Count.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto_ExistingRecords()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory());
            });
            var foodCategoryId = _context.FoodCategories.First().FoodCategoryId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.FoodItems.Add(DomainTestDataFactory.CreateFoodItem(foodCategoryId));
                ctx.MealSuggestions.Add(DomainTestDataFactory.CreateMealSuggestion());
            });
            var recipeId = _context.Recipes.First().RecipeId;
            var foodItemId = _context.FoodItems.First(x => x.FoodCategoryId == foodCategoryId).FoodItemId;
            var mealSuggestionId = _context.MealSuggestions.First().MealSuggestionId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.RecipeSteps.Add(DomainTestDataFactory.CreateRecipeStep(recipeId));
                ctx.Ingredients.Add(DomainTestDataFactory.CreateIngredient(recipeId, foodItemId));
            });
            var recipeStepId = _context.RecipeSteps.First(x => x.RecipeId == recipeId).StepId;
            var ingredientId = _context.Ingredients.First(x => x.RecipeId == recipeId && x.FoodItemId == foodItemId).IngredientId;

            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto(recipeId);

            recipeDto.Steps.Add(ApplicationTestDataFactory.CreateRecipeStepDto(recipeId, recipeStepId));
            recipeDto.MealSuggestions.Add(ApplicationTestDataFactory.CreateRecipeMealSuggestionDto(recipeId, mealSuggestionId));
            recipeDto.Ingredients.Add(ApplicationTestDataFactory.CreateIngredientDto(recipeId, foodItemId, ingredientId));

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(recipeId, recipeDto);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldNotBeNull();
            result.Item.RecipeId.ShouldBe(recipeId);
            result.Item.Title.ShouldBe(recipeDto.Title);
            result.Item.Steps.Count.ShouldBe(1);
            result.Item.MealSuggestions.Count.ShouldBe(1);
            result.Item.Ingredients.Count.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldReturnRecipe_WhenValidRecipeDto_DeleteRecords()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.FoodCategories.Add(DomainTestDataFactory.CreateFoodCategory());
            });
            var foodCategoryId = _context.FoodCategories.First().FoodCategoryId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
                ctx.FoodItems.Add(DomainTestDataFactory.CreateFoodItem(foodCategoryId));
                ctx.MealSuggestions.Add(DomainTestDataFactory.CreateMealSuggestion());
            });
            var recipeId = _context.Recipes.First().RecipeId;
            var foodItemId = _context.FoodItems.First(x => x.FoodCategoryId == foodCategoryId).FoodItemId;
            var mealSuggestionId = _context.MealSuggestions.First().MealSuggestionId;

            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.RecipeSteps.Add(DomainTestDataFactory.CreateRecipeStep(recipeId));
                ctx.Ingredients.Add(DomainTestDataFactory.CreateIngredient(recipeId, foodItemId));
            });
            var recipeStepId = _context.RecipeSteps.First(x => x.RecipeId == recipeId).StepId;
            var ingredientId = _context.Ingredients.First(x => x.RecipeId == recipeId && x.FoodItemId == foodItemId).IngredientId;

            var recipeDto = ApplicationTestDataFactory.CreateRecipeDto(recipeId);

            // Act
            var result = await _recipeUseCases.UpdateRecipeAsync(recipeId, recipeDto);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldNotBeNull();
            result.Item.RecipeId.ShouldBe(recipeId);
            result.Item.Title.ShouldBe(recipeDto.Title);
            result.Item.Steps.Count.ShouldBe(0);
            result.Item.MealSuggestions.Count.ShouldBe(0);
            result.Item.Ingredients.Count.ShouldBe(0);
        }

        [Fact]
        public async Task DeleteRecipeAsync_ShouldReturnRecipe_WhenValidRecipeId()
        {
            // Arrange
            DbContextHelper.SeedDatabase(_context, ctx =>
            {
                ctx.Recipes.Add(DomainTestDataFactory.CreateRecipe());
            });
            var recipeId = _context.Recipes.First().RecipeId;

            // Act
            var result = await _recipeUseCases.DeleteRecipeAsync(recipeId);

            // Assert
            result.ShouldNotBeNull();
            result.ErrorList.ShouldBeEmpty();
            result.Item.ShouldBeNull();
        }
    }
}
