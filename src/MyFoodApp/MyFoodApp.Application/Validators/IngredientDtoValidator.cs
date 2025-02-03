using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class IngredientDtoValidator : AbstractValidator<IngredientDto>
    {
        public IngredientDtoValidator()
        {
            RuleFor(x => x.FoodItemId)
                .NotEmpty().WithMessage("FoodItemId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
