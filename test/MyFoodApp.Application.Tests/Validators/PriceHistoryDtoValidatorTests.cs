using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using Xunit;

namespace MyFoodApp.Application.Tests.Validators
{
    public class PriceHistoryDtoValidatorTests
    {
        private readonly PriceHistoryDtoValidator _validator;

        public PriceHistoryDtoValidatorTests()
        {
            _validator = new PriceHistoryDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FoodItemId_Is_Empty()
        {
            var dto = new PriceHistoryDto { Price = 10, StartDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FoodItemId).WithErrorMessage("FoodItemId is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_NonPositive()
        {
            var dto = new PriceHistoryDto { FoodItemId = 1, Price = 0, StartDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("Price must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_StartDate_Is_Empty()
        {
            var dto = new PriceHistoryDto { FoodItemId = 1, Price = 10 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.StartDate).WithErrorMessage("StartDate is required.");
        }

        [Fact]
        public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
        {
            var dto = new PriceHistoryDto { FoodItemId = 1, Price = 10, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(-1) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.EndDate).WithErrorMessage("EndDate must be later than StartDate.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new PriceHistoryDto { FoodItemId = 1, Price = 10, StartDate = DateTime.Now };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
