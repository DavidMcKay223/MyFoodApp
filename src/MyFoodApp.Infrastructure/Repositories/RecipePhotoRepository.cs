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
    public class RecipePhotoRepository : IRecipePhotoRepository
    {
        private readonly AppDbContext _context;

        public RecipePhotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RecipePhoto recipePhoto)
        {
            _context.RecipePhotos.Add(recipePhoto);
            await _context.SaveChangesAsync();
        }
    }
}
