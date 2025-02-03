using Bogus;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Enums;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Tests.Data
{
    public static class ApplicationTestDataFactory
    {
        public static FoodCategoryDto CreateFoodCategoryDto(int? id = null)
        {
            return new Faker<FoodCategoryDto>()
                .RuleFor(fc => fc.FoodCategoryId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(fc => fc.Name, f => f.Commerce.Department())
                .RuleFor(fc => fc.Description, f => f.Lorem.Sentence())
                .Generate();
        }

        public static FoodItemDto CreateFoodItemDto(int categoryId, int? id = null)
        {
            return new Faker<FoodItemDto>()
                .RuleFor(fi => fi.FoodItemId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(fi => fi.Name, f => f.Commerce.ProductName())
                .RuleFor(fi => fi.Description, f => f.Commerce.ProductDescription())
                .RuleFor(fi => fi.FoodCategoryId, categoryId)
                .RuleFor(fi => fi.CaloriesPerUnit, f => f.Random.Decimal(50, 500))
                .RuleFor(fi => fi.ProteinPerUnit, f => f.Random.Decimal(0, 50))
                .Generate();
        }

        public static List<FoodItemDto> CreateFoodItemDtos(int count, int categoryId)
        {
            return new Faker<FoodItemDto>()
                .RuleFor(fi => fi.FoodItemId, f => f.IndexGlobal + 1)
                .RuleFor(fi => fi.Name, f => f.Commerce.ProductName())
                .RuleFor(fi => fi.Description, f => f.Commerce.ProductDescription())
                .RuleFor(fi => fi.FoodCategoryId, categoryId)
                .RuleFor(fi => fi.CaloriesPerUnit, f => f.Random.Decimal(50, 500))
                .RuleFor(fi => fi.ProteinPerUnit, f => f.Random.Decimal(0, 50))
                .Generate(count);
        }

        public static FoodItemStoreSectionDto CreateFoodItemStoreSectionDto(int foodItemId, int storeSectionId)
        {
            return new Faker<FoodItemStoreSectionDto>()
                .RuleFor(fiss => fiss.FoodItemId, foodItemId)
                .RuleFor(fiss => fiss.StoreSectionId, storeSectionId)
                .RuleFor(fiss => fiss.ShelfNumber, f => f.Random.Int(1, 100))
                .Generate();
        }

        public static IngredientDto CreateIngredientDto(int recipeId, int foodItemId)
        {
            return new Faker<IngredientDto>()
                .RuleFor(i => i.IngredientId, f => f.IndexGlobal + 1)
                .RuleFor(i => i.RecipeId, recipeId)
                .RuleFor(i => i.FoodItemId, foodItemId)
                .RuleFor(i => i.Quantity, f => f.Random.Decimal(1, 100))
                .RuleFor(i => i.Unit, f => f.PickRandom<UnitType>())
                .Generate();
        }

        public static MealSuggestionDto CreateMealSuggestionDto(int? id = null)
        {
            return new Faker<MealSuggestionDto>()
                .RuleFor(ms => ms.MealSuggestionId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(ms => ms.Name, f => f.Commerce.ProductName())
                .RuleFor(ms => ms.Description, f => f.Lorem.Sentence())
                .RuleFor(ms => ms.MealType, f => f.PickRandom<MealType>())
                .RuleFor(ms => ms.EffectiveDate, f => f.Date.Past())
                .RuleFor(ms => ms.ExpirationDate, f => f.Date.Future())
                .Generate();
        }

        public static MealSuggestionTagDto CreateMealSuggestionTagDto(int? id = null)
        {
            return new Faker<MealSuggestionTagDto>()
                .RuleFor(tag => tag.TagId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(tag => tag.TagName, f => f.Lorem.Word())
                .Generate();
        }

        public static PriceHistoryDto CreatePriceHistoryDto(int foodItemId, int? id = null)
        {
            return new Faker<PriceHistoryDto>()
                .RuleFor(ph => ph.PriceHistoryId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(ph => ph.FoodItemId, foodItemId)
                .RuleFor(ph => ph.Price, f => f.Random.Decimal(1, 100))
                .RuleFor(ph => ph.StartDate, f => f.Date.Past())
                .RuleFor(ph => ph.EndDate, f => f.Date.Future())
                .Generate();
        }

        public static RecipeDto CreateRecipeDto(int? id = null)
        {
            return new Faker<RecipeDto>()
                .RuleFor(r => r.RecipeId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(r => r.Title, f => f.Lorem.Sentence())
                .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
                .RuleFor(r => r.PrepTimeMinutes, f => f.Random.Int(10, 60))
                .RuleFor(r => r.CookTimeMinutes, f => f.Random.Int(10, 120))
                .RuleFor(r => r.Servings, f => f.Random.Int(1, 10))
                .Generate();
        }

        public static RecipeMealSuggestionDto CreateRecipeMealSuggestionDto(int recipeId, int mealSuggestionId)
        {
            return new Faker<RecipeMealSuggestionDto>()
                .RuleFor(rms => rms.RecipeId, recipeId)
                .RuleFor(rms => rms.MealSuggestionId, mealSuggestionId)
                .Generate();
        }

        public static RecipeStepDto CreateRecipeStepDto(int recipeId, int? id = null)
        {
            return new Faker<RecipeStepDto>()
                .RuleFor(rs => rs.StepId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(rs => rs.RecipeId, recipeId)
                .RuleFor(rs => rs.StepNumber, f => f.Random.Int(1, 10))
                .RuleFor(rs => rs.Instruction, f => f.Lorem.Sentence())
                .Generate();
        }

        public static StoreSectionDto CreateStoreSectionDto(int? id = null)
        {
            return new Faker<StoreSectionDto>()
                .RuleFor(ss => ss.StoreSectionId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(ss => ss.Name, f => f.Commerce.Department())
                .RuleFor(ss => ss.Description, f => f.Lorem.Sentence())
                .Generate();
        }
    }
}
