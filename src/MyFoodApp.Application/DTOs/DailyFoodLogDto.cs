using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFoodApp.Application.DTOs
{
    public class DailyFoodLogDto
    {
        public int LogId { get; set; }
        public required string UserId { get; set; }
        public DateTime LogDate { get; set; }
        public required byte[] ImageData { get; set; }
        public string? ImageContentType { get; set; }
        public string? Description { get; set; }

        // Navigation Properties
        public List<DailyFoodLogRecipeDto> LoggedRecipes { get; set; } = [];
    }
}
