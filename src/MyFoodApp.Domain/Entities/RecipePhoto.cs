using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyFoodApp.Domain.Entities
{
    public class RecipePhoto
    {
        [Key]
        [JsonIgnore]
        public int RecipePhotoId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [Required]
        public required byte[] ImageData { get; set; }

        [MaxLength(255)]
        public string? ImageContentType { get; set; }

        [MaxLength(500)]
        public string? Caption { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // Navigation
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}
