using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities.Authentication;
using MyFoodApp.Domain.Interfaces.Repositories.Authentication;
using MyFoodApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFoodApp.Infrastructure.Repositories.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null)
            {
                return false;
            }

            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            User? userToDelete = await _dbContext.Users.FindAsync(userId);
            if (userToDelete == null)
            {
                return false;
            }

            _dbContext.Users.Remove(userToDelete);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Update(user);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
