using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels
{
    public class ApiResponse<T>
    {
        public T Item { get; set; }
        public Error Error { get; set; }
    }
}
