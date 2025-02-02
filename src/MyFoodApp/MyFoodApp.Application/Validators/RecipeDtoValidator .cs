using FluentValidation;
using MyFoodApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Validators
{
    public class RecipeDtoValidator : AbstractValidator<RecipeDto>
    {
        public RecipeDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

            RuleFor(x => x.PrepTimeMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("PrepTimeMinutes must be a positive value.");

            RuleFor(x => x.CookTimeMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("CookTimeMinutes must be a positive value.");

            RuleFor(x => x.Servings)
                .GreaterThan(0).WithMessage("Servings must be greater than zero.");

            RuleForEach(x => x.Steps).SetValidator(new RecipeStepDtoValidator());
            RuleForEach(x => x.Ingredients).SetValidator(new IngredientDtoValidator());
            RuleForEach(x => x.MealSuggestions).SetValidator(new RecipeMealSuggestionDtoValidator());
        }
    }
}
