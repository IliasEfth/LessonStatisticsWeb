using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels
{
    public class MappedSemester
    {
        public virtual int Id { get; set; }
        public virtual int Start { get; set; }
        public virtual int End { get; set; }
        public virtual Department Department { get; set; }
    }
}
