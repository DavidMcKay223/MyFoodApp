using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class MealSuggestionDtoValidator : AbstractValidator<MealSuggestionDto>
    {
        public MealSuggestionDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.MealType)
                .NotNull().WithMessage("MealType is required.");

            RuleFor(x => x.EffectiveDate)
                .NotEmpty().WithMessage("EffectiveDate is required.");

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(x => x.EffectiveDate).When(x => x.ExpirationDate.HasValue)
                .WithMessage("ExpirationDate must be later than EffectiveDate.");
        }
    }
}
