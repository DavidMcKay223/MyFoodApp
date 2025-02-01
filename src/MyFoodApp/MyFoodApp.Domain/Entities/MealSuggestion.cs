﻿using MyFoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Domain.Entities
{
    public class MealSuggestion
    {
        [Key]
        public int MealSuggestionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public MealType MealType { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // Navigation
        public ICollection<RecipeMealSuggestion> RecipeSuggestions { get; set; }
        public ICollection<MealSuggestionTag> Tags { get; set; }
    }
}
