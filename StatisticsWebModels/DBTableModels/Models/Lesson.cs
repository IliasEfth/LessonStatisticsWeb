using System;
using System.Collections.Generic;
using System.Text;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;

namespace StatisticsWebModels
{
    public class Lesson
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Semester { get; set; }
        public virtual string Type { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual float ECTS { get; set; }        
    }
}