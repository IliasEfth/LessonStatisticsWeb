using StatisticsWebModels.DBTableModels.Interfaces.GradeInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class Grade : IGrade
    {
        public int Id { get; set; }
        public float Graded { get; set; }
        public virtual User User { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
