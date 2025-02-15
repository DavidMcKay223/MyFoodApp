using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFoodApp.Web.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private AuthenticationState _authState;

        public CustomAuthStateProvider()
        {
            _authState = _anonymousState;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(_authState);
        }

        public void MarkUserAsAuthenticated(string userId, string username)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "CustomAuthentication");
            var principal = new ClaimsPrincipal(identity);

            _authState = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _authState = _anonymousState;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
