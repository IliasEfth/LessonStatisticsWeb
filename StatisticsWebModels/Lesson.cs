using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Type { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}