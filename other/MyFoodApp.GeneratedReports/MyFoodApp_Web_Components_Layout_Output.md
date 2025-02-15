# Directory: Components\Layout

## File: MainLayout.razor

```C#
@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4">
            <a href="https://learn.microsoft.com/aspnet/core/" target="_blank">About</a>
        </div>

        <article class="content px-4">
            <CascadingAuthenticationState>
                @Body
            </CascadingAuthenticationState>
        </article>
    </main>
</div>

<div id="blazor-error-ui" data-nosnippet>
    An unhandled error has occurred.
    <a href="." class="reload">Reload</a>
    <span class="dismiss">🗙</span>
</div>

```

## File: NavMenu.razor

```C#
<div class="top-row ps-3 navbar navbar-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="">MyFoodApp.Web</a>
    </div>
</div>

<input type="checkbox" title="Navigation menu" class="navbar-toggler" />

<div class="nav-scrollable" onclick="document.querySelector('.navbar-toggler').click()">
    <nav class="nav flex-column">
        <AuthorizeView>
            <Authorized>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                        <span class="bi bi-house-door-fill-nav-menu" aria-hidden="true"></span> Home
                    </NavLink>
                </div>

                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="recipes">
                        <span class="bi bi-plus-square-fill-nav-menu" aria-hidden="true"></span> Recipes
                    </NavLink>
                </div>

                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="mealsuggestion">
                        <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Meal Suggestion
                    </NavLink>
                </div>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="profile"> <span class="oi oi-person" aria-hidden="true"></span> Profile </NavLink>
                </div>
                 <div class="nav-item px-3">
                    <NavLink class="nav-link" href="logout">
                        <span class="oi oi-account-logout" aria-hidden="true"></span> Logout
                    </NavLink>
                </div>
            </Authorized>
            <NotAuthorized>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="register">
                        <span class="oi oi-person-add" aria-hidden="true"></span> Register
                    </NavLink>
                </div>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="login">
                        <span class="oi oi-account-login" aria-hidden="true"></span> Login
                    </NavLink>
                </div>
            </NotAuthorized>
        </AuthorizeView>
    </nav>
</div>

```

