using Bogus;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.Tests.Data
{
    public static class ApplicationTestDataFactory
    {
        public static IngredientDto CreateIngredientDto(int? id = null, int? ingredientId = null)
        {
            return new Faker<IngredientDto>()
                .RuleFor(i => i.IngredientId, f => ingredientId ?? f.IndexGlobal + 1)
                .RuleFor(i => i.RecipeId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(i => i.FoodItemId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Quantity, f => f.Random.Decimal(1, 100))
                .RuleFor(i => i.Unit, f => f.PickRandom<UnitType>())
                .Generate();
        }

        public static RecipeStepDto CreateRecipeStepDto(int? id = null, int? stepId = null)
        {
            return new Faker<RecipeStepDto>()
                .RuleFor(i => i.StepId, f => stepId ?? f.IndexGlobal + 1)
                .RuleFor(s => s.RecipeId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(s => s.StepNumber, f => f.IndexGlobal + 1)
                .RuleFor(s => s.Instruction, f => f.Lorem.Sentence())
                .Generate();
        }

        public static RecipeMealSuggestionDto CreateRecipeMealSuggestionDto(int? id = null, int? mealSuggestionId = null)
        {
            return new Faker<RecipeMealSuggestionDto>()
                .RuleFor(m => m.RecipeId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(m => m.MealSuggestionId, f => mealSuggestionId ?? f.IndexGlobal)
                .Generate();
        }

        public static RecipeDto CreateRecipeDto(int? id = null)
        {
            return new Faker<RecipeDto>()
                .RuleFor(m => m.RecipeId, f => id ?? f.IndexGlobal + 1)
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                .RuleFor(r => r.PrepTimeMinutes, f => f.Random.Int(10, 60))
                .RuleFor(r => r.CookTimeMinutes, f => f.Random.Int(10, 60))
                .RuleFor(r => r.Servings, f => f.Random.Int(1, 10))
                .Generate();
        }
    }
}
