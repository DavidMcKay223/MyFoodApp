using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Tests.Data;

namespace MyFoodApp.Infrastructure.Tests.Helpers
{
    public static class DbContextHelper
    {
        private class TestDbContext : AppDbContext
        {
            public TestDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
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

        public static void SeedDatabase(AppDbContext context, Action<AppDbContext> seedAction)
        {
            seedAction(context);
            context.SaveChanges();
        }

        public static void CleanDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.CloseConnection();
            context.Dispose();
        }
    }
}
