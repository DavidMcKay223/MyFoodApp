using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyFoodApp.Web.Authentication
{
    public class SessionAuthenticationStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SessionAuthenticationStateService> _logger;
        private readonly string _userIdSessionKey = "UserId";

        public SessionAuthenticationStateService(
            IHttpContextAccessor httpContextAccessor,
            ILogger<SessionAuthenticationStateService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public Task<string?> GetUserIdFromSessionAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null.");
                return Task.FromResult<string?>(null);
            }

            var userId = httpContext.Session.GetString(_userIdSessionKey);
            _logger.LogInformation("Retrieved UserId from session: {UserId}", userId);
            return Task.FromResult(userId);
        }

        public Task SetUserIdInSessionAsync(string userId)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null.");
                return Task.CompletedTask;
            }

            httpContext.Session.SetString(_userIdSessionKey, userId);
            _logger.LogInformation("Set UserId in session: {UserId}", userId);
            return Task.CompletedTask;
        }

        public Task ClearSessionAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null.");
                return Task.CompletedTask;
            }

            httpContext.Session.Remove(_userIdSessionKey);
            _logger.LogInformation("Cleared UserId from session.");
            return Task.CompletedTask;
        }
    }
}
