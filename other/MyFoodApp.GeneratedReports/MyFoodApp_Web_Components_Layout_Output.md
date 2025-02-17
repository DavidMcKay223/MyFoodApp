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
            <AuthorizeView>
                <Authorized>
                    <div class="d-flex align-items-center gap-3">
                        <!-- User badge with icon -->
                        <NavLink href="/Account/Manage" class="text-decoration-none d-flex align-items-center gap-2">
                            <span class="bi bi-person-circle fs-5"></span>
                            <span class="badge bg-primary bg-opacity-10 text-primary rounded-pill">
                                @context.User.Identity?.Name
                            </span>
                        </NavLink>

                        <!-- Logout button -->
                        <form action="Account/Logout" method="post">
                            <AntiforgeryToken />
                            <input type="hidden" name="ReturnUrl" value="" />
                            <button type="submit" class="btn btn-outline-secondary d-flex align-items-center gap-1">
                                <span class="bi bi-arrow-bar-left"></span>
                                <span>Logout</span>
                            </button>
                        </form>
                    </div>
                </Authorized>
                <NotAuthorized>
                    <div class="d-flex align-items-center gap-3">
                        <p class="mb-0 text-muted">You are not logged in.</p>
                        <NavLink href="/Account/Login" class="btn btn-primary">
                            Login
                        </NavLink>
                    </div>
                </NotAuthorized>
            </AuthorizeView>
        </div>

        <article class="content px-4">
            @Body
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
            </Authorized>
            <NotAuthorized>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="/Account/Register">
                        <span class="oi oi-person-add" aria-hidden="true"></span> Register
                    </NavLink>
                </div>
                <div class="nav-item px-3">
                    <NavLink class="nav-link" href="/Account/Login">
                        <span class="oi oi-account-login" aria-hidden="true"></span> Login
                    </NavLink>
                </div>
            </NotAuthorized>
        </AuthorizeView>
    </nav>
</div>

```

