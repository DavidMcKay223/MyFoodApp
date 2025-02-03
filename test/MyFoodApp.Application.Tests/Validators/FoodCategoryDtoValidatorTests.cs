using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using Xunit;

namespace MyFoodApp.Application.Tests.Validators
{
    public class FoodCategoryDtoValidatorTests
    {
        private readonly FoodCategoryDtoValidator _validator;

        public FoodCategoryDtoValidatorTests()
        {
            _validator = new FoodCategoryDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var dto = new FoodCategoryDto { Name = string.Empty, Description = "Valid Description" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var dto = new FoodCategoryDto { Name = "Valid Name", Description = string.Empty };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Description).WithErrorMessage("Description is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new FoodCategoryDto { Name = "Valid Name", Description = "Valid Description" };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
