using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;

namespace MyFoodApp.Application.UseCases
{
    public class GenerateRecommendationsUseCases : IGenerateRecommendationsUseCases
    {
        private readonly IGenerateRecommendationsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GenerateRecommendationsUseCases> _logger;

        public GenerateRecommendationsUseCases(IGenerateRecommendationsRepository repository, IMapper mapper, ILogger<GenerateRecommendationsUseCases> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<MealSuggestionTagDto>> GetMealSuggestionWithRecipeListAsync()
        {
            var response = new Response<MealSuggestionTagDto>();

            try
            {
                var query = await _repository.GetAllMealSuggestionsTagsAsync().ToListAsync();

                response.List = _mapper.Map<List<MealSuggestionTagDto>>(query);
                response.TotalItems = response.List.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while looking up meal suggestion.");
                response.ErrorList.Add(new Error { Code = "LookupError", Message = ex.Message });
            }

            return response;
        }
    }
}
