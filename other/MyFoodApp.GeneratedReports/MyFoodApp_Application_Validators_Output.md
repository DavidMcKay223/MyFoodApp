# Directory: Validators

## File: FoodCategoryDtoValidator.cs

```
using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class FoodCategoryDtoValidator : AbstractValidator<FoodCategoryDto>
    {
        public FoodCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}

```

## File: FoodItemDtoValidator.cs

```
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

```

## File: FoodItemStoreSectionDtoValidator.cs

```
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

```

## File: IngredientDtoValidator.cs

```
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

```

## File: MealSuggestionDtoValidator.cs

```
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

```

## File: MealSuggestionTagDtoValidator.cs

```
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

```

## File: PriceHistoryDtoValidator.cs

```
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

```

## File: RecipeDtoValidator.cs

```
using FluentValidation;
using MyFoodApp.Application.DTOs;

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

```

## File: RecipeMealSuggestionDtoValidator.cs

```
using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class RecipeMealSuggestionDtoValidator : AbstractValidator<RecipeMealSuggestionDto>
    {
        public RecipeMealSuggestionDtoValidator()
        {
            RuleFor(x => x.MealSuggestionId)
                .NotEmpty().WithMessage("MealSuggestionId is required.");
        }
    }
}

```

## File: RecipeStepDtoValidator.cs

```
using FluentValidation;
using MyFoodApp.Application.DTOs;

namespace MyFoodApp.Application.Validators
{
    public class RecipeStepDtoValidator : AbstractValidator<RecipeStepDto>
    {
        public RecipeStepDtoValidator()
        {
            RuleFor(x => x.StepNumber)
                .GreaterThan(0).WithMessage("StepNumber must be greater than zero.");

            RuleFor(x => x.Instruction)
                .NotEmpty().WithMessage("Instruction is required.")
                .MaximumLength(1000).WithMessage("Instruction must not exceed 1000 characters.");
        }
    }
}

```

## File: StoreSectionDtoValidator.cs

```
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

```

