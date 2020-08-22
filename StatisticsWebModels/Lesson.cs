using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels
{
    public class Lesson
    {       
        public string Id { get; set; }
        public string Name { get; set; }
        public float? Grade { get; set; }
        public float ECTS { get; set; }
        public string Semester { get; set; }
    }
}
