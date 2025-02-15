# Directory: Interfaces\Repositories\Authentication

## File: IRoleRepository.cs

```C#
using MyFoodApp.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Interfaces.Repositories.Authentication
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(string roleId);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<bool> AddRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}

```

## File: IUserRepository.cs

```C#
using MyFoodApp.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Interfaces.Repositories.Authentication
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
    }
}

```

