using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using Xunit;

namespace MyFoodApp.Application.Tests.Validators
{
    public class RecipeMealSuggestionDtoValidatorTests
    {
        private readonly RecipeMealSuggestionDtoValidator _validator;

        public RecipeMealSuggestionDtoValidatorTests()
        {
            _validator = new RecipeMealSuggestionDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_RecipeId_Is_Empty()
        {
            var dto = new RecipeMealSuggestionDto { MealSuggestionId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.RecipeId).WithErrorMessage("RecipeId is required.");
        }

        [Fact]
        public void Should_Have_Error_When_MealSuggestionId_Is_Empty()
        {
            var dto = new RecipeMealSuggestionDto { RecipeId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.MealSuggestionId).WithErrorMessage("MealSuggestionId is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new RecipeMealSuggestionDto { RecipeId = 1, MealSuggestionId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
