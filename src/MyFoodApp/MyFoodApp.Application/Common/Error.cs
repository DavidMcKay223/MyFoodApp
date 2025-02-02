using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Common
{
    public class Error
    {
        public required string Code { get; set; }
        public required string Message { get; set; }
    }
}
