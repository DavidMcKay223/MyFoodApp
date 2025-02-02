﻿using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Mappings
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<RecipeMealSuggestion, RecipeMealSuggestionDto>().ReverseMap();
            CreateMap<RecipeStep, RecipeStepDto>().ReverseMap();
        }
    }
}
