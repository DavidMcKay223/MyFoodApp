using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MyFoodApp.Domain.Entities.Authentication;

namespace MyFoodApp.Domain.Entities
{
    public class DailyFoodLog
    {
        [Key]
        [JsonIgnore]
        public int LogId { get; set; }

        [ForeignKey("User")]
        [MaxLength(450)]
        public required string UserId { get; set; }

        public DateTime LogDate { get; set; } = DateTime.UtcNow.Date;

        [Required]
        public required byte[] ImageData { get; set; }

        [MaxLength(255)]
        public string? ImageContentType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation
        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<DailyFoodLogRecipe> LoggedRecipes { get; set; } = [];
    }
}
