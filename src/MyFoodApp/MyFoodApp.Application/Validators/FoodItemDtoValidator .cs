using FluentValidation;
using MyFoodApp.Application.DTOs;

public class FoodItemDtoValidator : AbstractValidator<FoodItemDto>
{
    public FoodItemDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.FoodCategoryId)
            .NotEmpty().WithMessage("FoodCategoryId is required.");

        RuleFor(x => x.CaloriesPerUnit)
            .GreaterThanOrEqualTo(0).WithMessage("CaloriesPerUnit must be a positive value.");

        RuleFor(x => x.ProteinPerUnit)
            .GreaterThanOrEqualTo(0).WithMessage("ProteinPerUnit must be a positive value.");
    }
}
