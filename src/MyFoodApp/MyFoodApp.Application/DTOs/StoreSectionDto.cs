using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs
{
    public class StoreSectionDto
    {
        public int StoreSectionId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
