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
    public class GeneratorPdfRepository : IGeneratorPdfRepository
    {
        private readonly AppDbContext _context;

        public GeneratorPdfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesByIdListAsync(List<int> idList)
        {
            return await _context.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f.FoodCategory)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f.PriceHistories)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.FoodItem)
                        .ThenInclude(f => f.StoreSections)
                            .ThenInclude(fi => fi.StoreSection)
                .Where(x => idList.Contains(x.RecipeId))
                .OrderBy(x => x.Title)
                .ToListAsync();
        }
    }
}
