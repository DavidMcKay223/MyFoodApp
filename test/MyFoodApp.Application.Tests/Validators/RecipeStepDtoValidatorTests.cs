using FluentValidation.TestHelper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Validators;

namespace MyFoodApp.Application.Tests.Validators
{
    public class RecipeStepDtoValidatorTests
    {
        private readonly RecipeStepDtoValidator _validator;

        public RecipeStepDtoValidatorTests()
        {
            _validator = new RecipeStepDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_StepNumber_Is_NonPositive()
        {
            var dto = new RecipeStepDto { StepNumber = 0, Instruction = "Valid Instruction" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.StepNumber).WithErrorMessage("StepNumber must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_Instruction_Is_Empty()
        {
            var dto = new RecipeStepDto { StepNumber = 1, Instruction = string.Empty };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Instruction).WithErrorMessage("Instruction is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Instruction_Exceeds_Max_Length()
        {
            var dto = new RecipeStepDto { StepNumber = 1, Instruction = new string('a', 1001) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Instruction).WithErrorMessage("Instruction must not exceed 1000 characters.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var dto = new RecipeStepDto { StepNumber = 1, Instruction = "Valid Instruction" };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
