using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Domain.Interfaces.Repositories;
using MyFoodApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Infrastructure.Repositories
{
    public class GenerateRecommendationsRepository : IGenerateRecommendationsRepository
    {
        private readonly AppDbContext _context;

        public GenerateRecommendationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<MealSuggestionTag> GetAllMealSuggestionsTagsAsync()
        {
            var recipes = _context.MealSuggestionTags
                .Include(x => x.MealSuggestions)
                    .ThenInclude(x => x.RecipeSuggestions)
                        .ThenInclude(x => x.Recipe)
                            .ThenInclude(x => x!.Ingredients)
                                .ThenInclude(x => x.FoodItem)
                .AsQueryable();

            return recipes;
        }
    }
}
