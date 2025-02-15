using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities.Authentication;
using MyFoodApp.Domain.Interfaces.Repositories.Authentication;
using MyFoodApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFoodApp.Infrastructure.Repositories.Authentication
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddRoleAsync(Role role)
        {
            if (role == null)
            {
                return false;
            }

            await _dbContext.Roles.AddAsync(role);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }

            var roleToDelete = await _dbContext.Roles.FindAsync(roleId);
            if (roleToDelete == null)
            {
                return false;
            }

            _dbContext.Roles.Remove(roleToDelete);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return null;
            }
            return await _dbContext.Roles.FindAsync(roleId);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            if (role == null)
            {
                return false;
            }

            _dbContext.Roles.Update(role);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
