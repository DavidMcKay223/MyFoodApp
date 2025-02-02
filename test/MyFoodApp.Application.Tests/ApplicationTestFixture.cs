using AutoMapper;
using MyFoodApp.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Tests
{
    public class ApplicationTestFixture
    {
        public IMapper Mapper { get; }

        public ApplicationTestFixture()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Register all AutoMapper profiles here
                cfg.AddProfile(new FoodProfile());
                cfg.AddProfile(new IngredientProfile());
                cfg.AddProfile(new MealProfile());
                cfg.AddProfile(new PriceProfile());
                cfg.AddProfile(new RecipeProfile());
                cfg.AddProfile(new StoreProfile());
            });

            Mapper = configuration.CreateMapper();
        }
    }

    [CollectionDefinition("ApplicationTestCollection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture> { }
}
