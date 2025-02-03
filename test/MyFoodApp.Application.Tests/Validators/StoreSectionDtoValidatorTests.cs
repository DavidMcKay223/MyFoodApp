using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;
using Xunit;

namespace MyFoodApp.Application.Tests.Validators
{
    public class StoreSectionDtoValidatorTests
    {
        private readonly StoreSectionDtoValidator _validator;

        public StoreSectionDtoValidatorTests()
        {
            _validator = new StoreSectionDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var dto = new StoreSectionDto { Name = string.Empty, Description = "Valid Description" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var dto = new StoreSectionDto { Name = new string('a', 101), Description = "Valid Description" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name must not exceed 100 characters.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_Max_Length()
        {
            var dto = new StoreSectionDto { Name = "Valid Name", Description = new string('a', 501) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Description).WithErrorMessage("Description must not exceed 500 characters.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new StoreSectionDto { Name = "Valid Name", Description = "Valid Description" };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
