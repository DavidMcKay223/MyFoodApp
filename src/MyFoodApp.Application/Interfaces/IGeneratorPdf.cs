using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Interfaces
{
    public interface IGeneratorPdf
    {
        public Task<byte[]> GenerateRecipeListPdfAsync(List<int> recipeIdList);
    }
}
