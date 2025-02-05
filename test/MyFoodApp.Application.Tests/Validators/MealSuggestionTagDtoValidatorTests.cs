using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;

namespace MyFoodApp.Application.Tests.Validators
{
    public class MealSuggestionTagDtoValidatorTests
    {
        private readonly MealSuggestionTagDtoValidator _validator;

        public MealSuggestionTagDtoValidatorTests()
        {
            _validator = new MealSuggestionTagDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_TagName_Is_Empty()
        {
            var dto = new MealSuggestionTagDto { TagName = string.Empty };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.TagName).WithErrorMessage("TagName is required.");
        }

        [Fact]
        public void Should_Have_Error_When_TagName_Exceeds_Max_Length()
        {
            var dto = new MealSuggestionTagDto { TagName = new string('a', 51) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.TagName).WithErrorMessage("TagName must not exceed 50 characters.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new MealSuggestionTagDto { TagName = "Valid Tag" };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
