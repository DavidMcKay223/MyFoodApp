using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Tests.Validators
{
    public class FoodItemDtoValidatorTests
    {
        private readonly FoodItemDtoValidator _validator;

        public FoodItemDtoValidatorTests()
        {
            _validator = new FoodItemDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var dto = new FoodItemDto { Name = string.Empty, Description = "Valid Description", FoodCategoryId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var dto = new FoodItemDto { Name = "Valid Name", Description = string.Empty, FoodCategoryId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Description).WithErrorMessage("Description is required.");
        }

        [Fact]
        public void Should_Have_Error_When_FoodCategoryId_Is_Zero()
        {
            var dto = new FoodItemDto { Name = "Valid Name", Description = "Valid Description", FoodCategoryId = 0 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FoodCategoryId).WithErrorMessage("FoodCategoryId is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new FoodItemDto { Name = "Valid Name", Description = "Valid Description", FoodCategoryId = 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
