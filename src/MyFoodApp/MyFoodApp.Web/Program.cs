using Blazored.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFoodApp.Application.Configurations;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Application.UseCases;
using MyFoodApp.Application.Validators;
using MyFoodApp.Domain.Entities.Authentication;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Domain.Interfaces.Repositories.Authentication;
using MyFoodApp.Infrastructure.Persistence;
using MyFoodApp.Infrastructure.Repositories;
using MyFoodApp.Infrastructure.Repositories.Authentication;
using MyFoodApp.Web.Components;
using Microsoft.Extensions.Logging;
using MyFoodApp.Web.Components.Account;

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
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Database:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("MyFoodApp.Infrastructure");
        }),
        ServiceLifetime.Scoped);

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
