using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
