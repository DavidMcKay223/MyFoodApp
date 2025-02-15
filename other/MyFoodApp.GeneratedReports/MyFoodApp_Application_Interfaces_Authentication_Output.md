# Directory: Interfaces\Authentication

## File: IAuthenticationUseCases.cs

```C#
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces.Authentication
{
    public interface IAuthenticationUseCases
    {
        Task<Response<UserDto>> RegisterUserAsync(RegisterUserDto registerDto);
        Task<Response<UserDto>> LoginUserAsync(LoginUserDto loginDto);
        Task<Response<bool>> LogoutAsync();
        Task<Response<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    }
}

```

