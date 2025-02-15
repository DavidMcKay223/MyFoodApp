# Directory: Components\Authentication

## File: CustomAuthStateProvider.cs

```C#
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

```

## File: Login.razor

```C#
@page "/login"
@rendermode InteractiveServer
@using MyFoodApp.Application.Interfaces.Authentication
@using MyFoodApp.Application.DTOs.Authentication
@inject IAuthenticationUseCases AuthenticationUseCases
@inject NavigationManager NavigationManager

<PageTitle>Login</PageTitle>

<h3>Login</h3>

@if (!string.IsNullOrEmpty(errorMessage))
{
    <div class="alert alert-danger">
        @errorMessage
    </div>
}

<EditForm Model="@loginDto" OnValidSubmit="@HandleLogin">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <div class="mb-3">
        <label for="usernameOrEmail" class="form-label">Username or Email</label>
        <InputText id="usernameOrEmail" class="form-control" @bind-Value="loginDto.UsernameOrEmail" />
        <ValidationMessage For="@(() => loginDto.UsernameOrEmail)" />
    </div>

    <div class="mb-3">
        <label for="password" class="form-label">Password</label>
        <InputText id="password" type="password" class="form-control" @bind-Value="loginDto.Password" />
        <ValidationMessage For="@(() => loginDto.Password)" />
    </div>

    <button type="submit" class="btn btn-primary">Login</button>
</EditForm>

@code {
    private LoginUserDto loginDto = new LoginUserDto();
    private string? errorMessage;

    private async Task HandleLogin()
    {
        errorMessage = null;

        var response = await AuthenticationUseCases.LoginUserAsync(loginDto);

        if (response.ErrorList.Any())
        {
            errorMessage = string.Join("\r\n", response.ErrorList.Select(e => e.Message));
        }
        else if (response.Item != null)
        {
            NavigationManager.NavigateTo("/");
        }
        else
        {
            errorMessage = "Login failed unexpectedly.";
        }
    }
}

```

## File: Register.razor

```C#
@page "/register"
@rendermode InteractiveServer
@using MyFoodApp.Application.Interfaces.Authentication
@using MyFoodApp.Application.DTOs.Authentication
@inject IAuthenticationUseCases AuthenticationUseCases
@inject NavigationManager NavigationManager

<PageTitle>Register</PageTitle>

<h3>Register</h3>

@if (!string.IsNullOrEmpty(errorMessage))
{
    <div class="alert alert-danger">
        @errorMessage
    </div>
}

<EditForm Model="@registerDto" OnValidSubmit="@HandleRegistration">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <div class="mb-3">
        <label for="username" class="form-label">Username</label>
        <InputText id="username" class="form-control" @bind-Value="registerDto.Username" />
        <ValidationMessage For="@(() => registerDto.Username)" />
    </div>

    <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <InputText id="email" type="email" class="form-control" @bind-Value="registerDto.Email" />
        <ValidationMessage For="@(() => registerDto.Email)" />
    </div>

    <div class="mb-3">
        <label for="password" class="form-label">Password</label>
        <InputText id="password" type="password" class="form-control" @bind-Value="registerDto.Password" />
        <ValidationMessage For="@(() => registerDto.Password)" />
    </div>

    <button type="submit" class="btn btn-primary">Register</button>
    <p class="mt-3">
        Already have an account? <a href="/login">Login</a>
    </p>
</EditForm>

@code {
    private RegisterUserDto registerDto = new RegisterUserDto();
    private string? errorMessage;

    private async Task HandleRegistration()
    {
        errorMessage = null;

        var response = await AuthenticationUseCases.RegisterUserAsync(registerDto);

        if (response.ErrorList.Any())
        {
            errorMessage = string.Join("<br>", response.ErrorList.Select(e => e.Message));
        }
        else if (response.Item != null)
        {
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            errorMessage = "Registration failed unexpectedly.";
        }
    }
}
```

