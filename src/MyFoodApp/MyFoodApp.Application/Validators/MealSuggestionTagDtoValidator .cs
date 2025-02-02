using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class MealSuggestionTagDtoValidator : AbstractValidator<MealSuggestionTagDto>
    {
        public MealSuggestionTagDtoValidator()
        {
            RuleFor(x => x.TagName)
                .NotEmpty().WithMessage("TagName is required.")
                .MaximumLength(50).WithMessage("TagName must not exceed 50 characters.");
        }
    }
}
