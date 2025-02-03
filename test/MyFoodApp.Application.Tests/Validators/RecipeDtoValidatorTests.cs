using FluentValidation.TestHelper;
using MyFoodApp.Application.Validators;

namespace MyFoodApp.Application.Tests.Validators
{
    public class RecipeDtoValidatorTests
    {
        private readonly RecipeDtoValidator _validator;

        public RecipeDtoValidatorTests()
        {
            _validator = new RecipeDtoValidator();
        }


    }
}
