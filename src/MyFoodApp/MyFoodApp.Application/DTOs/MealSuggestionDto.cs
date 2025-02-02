using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class MealSuggestionDto
    {
        public int MealSuggestionId { get; set; }
        public required string Name { get; set; }
        public MealType MealType { get; set; }
        public required string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
