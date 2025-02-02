using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class RecipeMealSuggestionDtoValidator : AbstractValidator<RecipeMealSuggestionDto>
    {
        public RecipeMealSuggestionDtoValidator()
        {
            RuleFor(x => x.RecipeId)
                .NotEmpty().WithMessage("RecipeId is required.");

            RuleFor(x => x.MealSuggestionId)
                .NotEmpty().WithMessage("MealSuggestionId is required.");
        }
    }
}
