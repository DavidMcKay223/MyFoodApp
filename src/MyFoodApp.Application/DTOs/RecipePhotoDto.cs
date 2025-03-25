using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class RecipePhotoDto
    {
        public int RecipePhotoId { get; set; }
        public int RecipeId { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageContentType { get; set; }
        public string? Caption { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
