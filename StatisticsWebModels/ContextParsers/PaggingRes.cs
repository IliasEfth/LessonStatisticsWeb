using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.ContextParsers
{
    public class PaggingRes<T>
    {        
        public virtual int Total { get; set; }
        public virtual T Items { get; set; }
    }
}
