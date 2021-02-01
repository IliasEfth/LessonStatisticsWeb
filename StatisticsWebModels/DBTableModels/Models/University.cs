using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class University
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
