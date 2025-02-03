using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Infrastructure.Persistence;

namespace MyFoodApp.Infrastructure.Tests
{
    public class SqliteTestBase : IDisposable
    {
        protected readonly AppDbContext _context;
        private readonly SqliteConnection _connection;

        public SqliteTestBase()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
        }

        protected void Seed(Action<AppDbContext> seedAction)
        {
            seedAction(_context);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _connection.Close();
            GC.SuppressFinalize(this);
        }
    }
}
