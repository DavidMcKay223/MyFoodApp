using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFoodApp.Application.Common;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;

namespace MyFoodApp.Application.UseCases
{
    public class MealSuggestionUseCases : IMealSuggestionUseCases
    {
        private readonly IMealSuggestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MealSuggestionUseCases> _logger;

        public MealSuggestionUseCases(IMealSuggestionRepository repository, IMapper mapper, ILogger<MealSuggestionUseCases> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<MealSuggestionDto>> GetMealSuggestionByIdAsync(int mealSuggestionId)
        {
            var response = new Response<MealSuggestionDto>();

            try
            {
                var mealSuggestion = await _repository.GetMealSuggestionByIdAsync(mealSuggestionId);
                if (mealSuggestion == null)
                {
                    response.ErrorList.Add(new Error { Code = "NotFound", Message = "Meal Suggestion not found." });
                    return response;
                }

                response.Item = _mapper.Map<MealSuggestionDto>(mealSuggestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting recipe by id.");
                response.ErrorList.Add(new Error { Code = "GetByIdError", Message = ex.Message });
            }

            return response;
        }

        public async Task<Response<MealSuggestionDto>> GetMealSuggestionListAsync()
        {
            var response = new Response<MealSuggestionDto>();

            try
            {
                var query = await _repository.GetAllMealSuggestionsAsync().ToListAsync();

                response.List = _mapper.Map<List<MealSuggestionDto>>(query);
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
