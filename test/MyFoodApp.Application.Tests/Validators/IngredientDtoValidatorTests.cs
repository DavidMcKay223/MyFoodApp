using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using MyFoodApp.Domain.Enums;

namespace MyFoodApp.Application.Tests.Validators
{
    public class IngredientDtoValidatorTests
    {
        private readonly IngredientDtoValidator _validator;

        public IngredientDtoValidatorTests()
        {
            _validator = new IngredientDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FoodItemId_Is_Empty()
        {
            var dto = new IngredientDto { Quantity = 1, Unit = UnitType.Gram };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FoodItemId).WithErrorMessage("FoodItemId is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_NonPositive()
        {
            var dto = new IngredientDto { FoodItemId = 1, Quantity = 0, Unit = UnitType.Gram };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Quantity).WithErrorMessage("Quantity must be greater than zero.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new IngredientDto { FoodItemId = 1, Quantity = 1, Unit = UnitType.Gram };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
