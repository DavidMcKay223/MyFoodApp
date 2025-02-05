using AutoMapper;
using MyFoodApp.Application.Mappings;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Tests.Helpers;

namespace MyFoodApp.Application.Tests
{
    public class ApplicationTestFixture : IDisposable
    {
        public IMapper Mapper { get; }
        public AppDbContext DbContext { get; }
        public IRecipeRepository RecipeRepository { get; }

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

            DbContext = DbContextHelper.CreateTestContext();

            RecipeRepository = new RecipeRepository(DbContext);
        }

        public void Dispose()
        {
            DbContextHelper.CleanDatabase(DbContext);
        }
    }

    [CollectionDefinition("ApplicationTestCollection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture> { }
}
