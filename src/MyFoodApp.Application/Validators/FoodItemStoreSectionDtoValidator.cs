using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class FoodItemStoreSectionDtoValidator : AbstractValidator<FoodItemStoreSectionDto>
    {
        public FoodItemStoreSectionDtoValidator()
        {
            RuleFor(x => x.FoodItemId)
                .NotEmpty().WithMessage("FoodItemId is required.");

            RuleFor(x => x.StoreSectionId)
                .NotEmpty().WithMessage("StoreSectionId is required.");

            RuleFor(x => x.ShelfNumber)
                .GreaterThanOrEqualTo(0).WithMessage("ShelfNumber must be a positive value.");
        }
    }
}
