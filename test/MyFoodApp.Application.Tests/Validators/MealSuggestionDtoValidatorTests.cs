using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using MyFoodApp.Domain.Enums;
using Xunit;

namespace MyFoodApp.Application.Tests.Validators
{
    public class MealSuggestionDtoValidatorTests
    {
        private readonly MealSuggestionDtoValidator _validator;

        public MealSuggestionDtoValidatorTests()
        {
            _validator = new MealSuggestionDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var dto = new MealSuggestionDto { Name = string.Empty, Description = "Valid Description", MealType = MealType.Breakfast, EffectiveDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var dto = new MealSuggestionDto { Name = "Valid Name", Description = string.Empty, MealType = MealType.Breakfast, EffectiveDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Description).WithErrorMessage("Description is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ExpirationDate_Is_Before_EffectiveDate()
        {
            var dto = new MealSuggestionDto { Name = "Valid Name", Description = "Valid Description", MealType = MealType.Breakfast, EffectiveDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(-1) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.ExpirationDate).WithErrorMessage("ExpirationDate must be later than EffectiveDate.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new MealSuggestionDto { Name = "Valid Name", Description = "Valid Description", MealType = MealType.Breakfast, EffectiveDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
