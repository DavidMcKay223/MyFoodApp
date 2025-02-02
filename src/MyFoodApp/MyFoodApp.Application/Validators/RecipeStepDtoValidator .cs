using FluentValidation;
using MyFoodApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Validators
{
    public class RecipeStepDtoValidator : AbstractValidator<RecipeStepDto>
    {
        public RecipeStepDtoValidator()
        {
            RuleFor(x => x.RecipeId)
                .NotEmpty().WithMessage("RecipeId is required.");

            RuleFor(x => x.StepNumber)
                .GreaterThan(0).WithMessage("StepNumber must be greater than zero.");

            RuleFor(x => x.Instruction)
                .NotEmpty().WithMessage("Instruction is required.")
                .MaximumLength(1000).WithMessage("Instruction must not exceed 1000 characters.");
        }
    }
}
