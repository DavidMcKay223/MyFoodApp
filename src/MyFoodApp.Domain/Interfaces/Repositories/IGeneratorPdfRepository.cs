﻿using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Interfaces.Repositories
{
    public interface IGeneratorPdfRepository
    {
        Task<List<Recipe>> GetAllRecipesByIdListAsync(List<int> idList);
    }
}
