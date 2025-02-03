using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
        private readonly SqliteConnection _connection;

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

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new AppDbContext(options);
            DbContext.Database.EnsureCreated();

            // Register Use Cases for Other Unit Test:
            RecipeRepository = new RecipeRepository(DbContext);
        }

        public void Dispose()
        {
            DbContextHelper.CleanDatabase(DbContext);
            _connection.Dispose();
        }
    }

    [CollectionDefinition("ApplicationTestCollection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture> { }
}
