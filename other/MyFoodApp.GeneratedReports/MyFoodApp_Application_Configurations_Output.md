# Directory: Configurations

## File: AutoMapperConfiguration.cs

```C#
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyFoodApp.Application.Mappings;

namespace MyFoodApp.Application.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Register all AutoMapper profiles here
                cfg.AddProfile(new FoodProfile());
                cfg.AddProfile(new IngredientProfile());
                cfg.AddProfile(new MealProfile());
                cfg.AddProfile(new PriceProfile());
                cfg.AddProfile(new RecipeProfile());
                cfg.AddProfile(new StoreProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

```

