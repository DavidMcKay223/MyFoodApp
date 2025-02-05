using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;

namespace MyFoodApp.Application.Tests.Validators
{
    public class FoodItemStoreSectionDtoValidatorTests
    {
        private readonly FoodItemStoreSectionDtoValidator _validator;

        public FoodItemStoreSectionDtoValidatorTests()
        {
            _validator = new FoodItemStoreSectionDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FoodItemId_Is_Empty()
        {
            var dto = new FoodItemStoreSectionDto { StoreSectionId = 1, ShelfNumber = 10 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FoodItemId).WithErrorMessage("FoodItemId is required.");
        }

        [Fact]
        public void Should_Have_Error_When_StoreSectionId_Is_Empty()
        {
            var dto = new FoodItemStoreSectionDto { FoodItemId = 1, ShelfNumber = 10 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.StoreSectionId).WithErrorMessage("StoreSectionId is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ShelfNumber_Is_Negative()
        {
            var dto = new FoodItemStoreSectionDto { FoodItemId = 1, StoreSectionId = 1, ShelfNumber = -1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.ShelfNumber).WithErrorMessage("ShelfNumber must be a positive value.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new FoodItemStoreSectionDto { FoodItemId = 1, StoreSectionId = 1, ShelfNumber = 10 };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
