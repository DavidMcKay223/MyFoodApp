# Directory: UseCases\Authentication

## File: AuthenticationUseCases.cs

```C#
using Microsoft.AspNetCore.Identity;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs.Authentication;
using MyFoodApp.Domain.Entities.Authentication;
using MyFoodApp.Application.Interfaces.Authentication;
using System.Linq;
using System.Threading.Tasks;

namespace MyFoodApp.Application.UseCases.Authentication
{
    public class AuthenticationUseCases : IAuthenticationUseCases
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationUseCases(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<UserDto>> RegisterUserAsync(RegisterUserDto registerDto)
        {
            var response = new Response<UserDto>();

            if (registerDto == null)
            {
                response.ErrorList.Add(new Error { Code = "InvalidInput", Message = "Invalid registration data provided." });
                return response;
            }

            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                response.Item = userDto;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.ErrorList.Add(new Error { Code = error.Code, Message = error.Description });
                }
            }

            return response;
        }

        public async Task<Response<UserDto>> LoginUserAsync(LoginUserDto loginDto)
        {
            var response = new Response<UserDto>();

            if (loginDto == null)
            {
                response.ErrorList.Add(new Error { Code = "InvalidInput", Message = "Invalid login data provided." });
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.UsernameOrEmail, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
                if (user == null)
                {
                    response.ErrorList.Add(new Error { Code = "UserNotFound", Message = "Failed to retrieve user after login." });
                    return response;
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                response.Item = userDto;
            }
            else
            {
                response.ErrorList.Add(new Error { Code = "LoginFailed", Message = "Invalid login attempt." });
            }

            return response;
        }

        public async Task<Response<bool>> LogoutAsync()
        {
            var response = new Response<bool>();

            // TODO: Implement Logout logic (e.g., _signInManager.SignOutAsync())
            response.ErrorList.Add(new Error { Code = "NotImplemented", Message = "Logout functionality is not yet implemented." });
            return response;
        }

        public async Task<Response<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var response = new Response<bool>();

            // TODO: Implement Forgot Password initiation logic (e.g., generate token, send email)
            response.ErrorList.Add(new Error { Code = "NotImplemented", Message = "Forgot Password functionality is not yet implemented." });
            return response;
        }
    }
}

```

