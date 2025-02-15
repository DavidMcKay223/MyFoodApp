using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MyFoodApp.Domain.Entities.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace MyFoodApp.Web.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly SessionAuthenticationStateService _sessionAuthService;
        private AuthenticationState _authState;

        private AuthenticationState _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        // Modify constructor
        public CustomAuthStateProvider(UserManager<User> userManager, SessionAuthenticationStateService sessionAuthService)
        {
            _userManager = userManager;
            _sessionAuthService = sessionAuthService;
            _authState = _anonymousState;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userId = await _sessionAuthService.GetUserIdFromSessionAsync();
                AuthenticationState newAuthState;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // Add roles or other claims here
                        var roles = await _userManager.GetRolesAsync(user);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                        var identity = new ClaimsIdentity(claims, "CustomAuthentication");
                        var principal = new ClaimsPrincipal(identity);
                        newAuthState = new AuthenticationState(principal);
                    }
                    else
                    {
                        newAuthState = _anonymousState;
                    }
                }
                else
                {
                    newAuthState = _anonymousState;
                }

                // Check if the state has changed
                if (!AreAuthenticationStatesEqual(_authState, newAuthState))
                {
                    _authState = newAuthState;
                    NotifyAuthenticationStateChanged(Task.FromResult(_authState));
                }

                return _authState;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return _anonymousState;
            }
        }

        private bool AreAuthenticationStatesEqual(AuthenticationState a, AuthenticationState b)
        {
            var aUser = a.User;
            var bUser = b.User;

            if (aUser.Identity.IsAuthenticated != bUser.Identity.IsAuthenticated)
                return false;

            if (!aUser.Identity.IsAuthenticated)
                return true;

            return aUser.FindFirst(ClaimTypes.NameIdentifier)?.Value
                == bUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task MarkUserAsAuthenticated(string userId, string username)
        {
            await _sessionAuthService.SetUserIdInSessionAsync(userId);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "CustomAuthentication");
            var principal = new ClaimsPrincipal(identity);
            _authState = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(_authState));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _sessionAuthService.ClearSessionAsync();
            _authState = _anonymousState;
            NotifyAuthenticationStateChanged(Task.FromResult(_authState));
        }
    }
}
