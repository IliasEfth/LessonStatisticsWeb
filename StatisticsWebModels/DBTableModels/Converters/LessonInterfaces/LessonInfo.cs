using StatisticsWebModels.DBTableModels.Converters.GradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.DBTableModels.Converters.LessonConverter
{
    public class LessonInfo
    {
        public LessonInfo() { }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Semester { get; set; }        
        public virtual float ECTS { get; set; }
        public virtual float Grades { get; set; }
        public virtual string Type { get; set; }        
    }
}
