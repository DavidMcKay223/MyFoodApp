using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.Common
{
    public class Response<T>
    {
        public T? Item { get; set; }
        public required List<T> List { get; set; }
        public int TotalItems { get; set; } = 0;
        public required List<Error> ErrorList { get; set; }
    }
}
