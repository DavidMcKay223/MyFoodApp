using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class PriceHistoryDtoValidator : AbstractValidator<PriceHistoryDto>
    {
        public PriceHistoryDtoValidator()
        {
            RuleFor(x => x.FoodItemId)
                .NotEmpty().WithMessage("FoodItemId is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue)
                .WithMessage("EndDate must be later than StartDate.");
        }
    }
}
