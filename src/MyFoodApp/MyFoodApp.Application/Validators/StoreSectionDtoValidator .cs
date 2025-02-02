using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class StoreSectionDtoValidator : AbstractValidator<StoreSectionDto>
    {
        public StoreSectionDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
