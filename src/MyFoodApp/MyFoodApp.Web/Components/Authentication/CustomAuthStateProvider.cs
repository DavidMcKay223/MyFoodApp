using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using MyFoodApp.Domain.Entities.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFoodApp.Web.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly UserManager<User> _userManager;
        private AuthenticationState _authState; // Store the authentication state

        private AuthenticationState _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthStateProvider(IJSRuntime jsRuntime, UserManager<User> userManager)
        {
            _jsRuntime = jsRuntime;
            _userManager = userManager;
            _authState = _anonymousState; // Initialize with anonymous state
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return _authState; // Return the stored authentication state
        }

        public async Task InitializeAuthenticationStateAsync()
        {
            try
            {
                // Retrieve the user ID from localStorage in OnAfterRenderAsync
                var userId = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userId");

                if (!string.IsNullOrEmpty(userId))
                {
                    // Fetch the user from the database
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var claims = new[] {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                        var identity = new ClaimsIdentity(claims, "CustomAuthentication");
                        var principal = new ClaimsPrincipal(identity);
                        _authState = new AuthenticationState(principal); // Update stored state
                        NotifyAuthenticationStateChanged(Task.FromResult(_authState)); // Notify state change
                        return;
                    }
                }
            }
            catch (JSException)
            {
                // Handle JS interop errors gracefully, especially during prerendering.
                // Log or ignore as appropriate for your application.
                Console.WriteLine("JS Interop error during InitializeAuthenticationStateAsync. This might be during prerendering.");
            }

            // If no user is found or an error occurred, set to anonymous state
            _authState = _anonymousState; // Ensure anonymous state is set
            NotifyAuthenticationStateChanged(Task.FromResult(_authState)); // Notify state change
        }


        public async Task MarkUserAsAuthenticated(string userId, string username)
        {
            // Store the user ID in localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", userId);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "CustomAuthentication");
            var principal = new ClaimsPrincipal(identity);
            _authState = new AuthenticationState(principal); // Update stored state
            NotifyAuthenticationStateChanged(Task.FromResult(_authState)); // Notify state change
        }

        public async Task MarkUserAsLoggedOut()
        {
            // Remove the user ID from localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userId");
            _authState = _anonymousState; // Update stored state to anonymous
            NotifyAuthenticationStateChanged(Task.FromResult(_authState)); // Notify state change
        }
    }
}
