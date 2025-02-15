using Blazored.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Application.Configurations;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Application.Interfaces.Authentication;
using MyFoodApp.Application.UseCases;
using MyFoodApp.Application.UseCases.Authentication;
using MyFoodApp.Application.Validators;
using MyFoodApp.Domain.Entities.Authentication;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Domain.Interfaces.Repositories.Authentication;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Repositories.Authentication;
using MyFoodApp.Web.Authentication;
using MyFoodApp.Web.Components;
using MyFoodApp.Web.Components.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add FluentValidation services
builder.Services.AddScoped<FluentValidationValidator>();
builder.Services.AddScoped<IValidator<FoodCategoryDto>, FoodCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<FoodItemDto>, FoodItemDtoValidator>();
builder.Services.AddScoped<IValidator<FoodItemStoreSectionDto>, FoodItemStoreSectionDtoValidator>();
builder.Services.AddScoped<IValidator<IngredientDto>, IngredientDtoValidator>();
builder.Services.AddScoped<IValidator<MealSuggestionDto>, MealSuggestionDtoValidator>();
builder.Services.AddScoped<IValidator<MealSuggestionTagDto>, MealSuggestionTagDtoValidator>();
builder.Services.AddScoped<IValidator<PriceHistoryDto>, PriceHistoryDtoValidator>();
builder.Services.AddScoped<IValidator<RecipeDto>, RecipeDtoValidator>();
builder.Services.AddScoped<IValidator<RecipeMealSuggestionDto>, RecipeMealSuggestionDtoValidator>();
builder.Services.AddScoped<IValidator<RecipeStepDto>, RecipeStepDtoValidator>();
builder.Services.AddScoped<IValidator<StoreSectionDto>, StoreSectionDtoValidator>();

// Services:
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeUseCases, RecipeUseCases>();
builder.Services.AddScoped<INutritionCalculatorService, NutritionCalculatorService>();

builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<IFoodItemUseCases, FoodItemUseCases>();

builder.Services.AddScoped<IMealSuggestionRepository, MealSuggestionRepository>();
builder.Services.AddScoped<IMealSuggestionUseCases, MealSuggestionUseCases>();

builder.Services.AddScoped<IGenerateRecommendationsRepository, GenerateRecommendationsRepository>();
builder.Services.AddScoped<IGenerateRecommendationsUseCases, GenerateRecommendationsUseCases>();

builder.Services.AddScoped<IGeneratorPdfRepository, GeneratorPdfRepository>();
builder.Services.AddScoped<IGeneratorPdf, GeneratorPdf>();

// Authentication:
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IAuthenticationUseCases, AuthenticationUseCases>();
builder.Services.AddScoped<SessionAuthenticationStateService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Database:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("MyFoodApp.Infrastructure");
        }),
        ServiceLifetime.Scoped);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseSession();

//begin SetString() hack
//app.Use(async delegate (HttpContext Context, Func<Task> Next)
//{
//    //this throwaway session variable will "prime" the SetString() method
//    //to allow it to be called after the response has started
//    var TempKey = Guid.NewGuid().ToString(); //create a random key
//    Context.Session.Set(TempKey, Array.Empty<byte>()); //set the throwaway session variable
//    Context.Session.Remove(TempKey); //remove the throwaway session variable
//    await Next(); //continue on with the request
//});
//end SetString() hack

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
