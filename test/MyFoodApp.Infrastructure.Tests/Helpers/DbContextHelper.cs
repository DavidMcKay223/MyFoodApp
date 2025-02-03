using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.Infrastructure.Tests.Helpers
{
    public static class DbContextHelper
    {
        // Create a derived context for testing configurations
        private class TestDbContext : AppDbContext
        {
            public TestDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configure test-specific model overrides
                modelBuilder.Entity<FoodItem>(entity =>
                {
                    entity.Property(e => e.FoodItemId)
                        .ValueGeneratedNever();
                });

                modelBuilder.Entity<StoreSection>(entity =>
                {
                    entity.Property(e => e.StoreSectionId)
                        .ValueGeneratedNever();
                });
            }
        }

        public static AppDbContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new TestDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }

        public static void CleanDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.CloseConnection();
            context.Dispose();
        }
    }
}
