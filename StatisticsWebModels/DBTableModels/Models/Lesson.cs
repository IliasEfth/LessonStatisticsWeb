using System;
using System.Collections.Generic;
using System.Text;
using StatisticsWebModels.DBTableModels.Interfaces.LessonInterfaces;

namespace StatisticsWebModels
{
    public class Lesson : ILesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Type { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public float ECTS { get; set; }        
    }
}