using StatisticsWebModels.DBTableModels.Converters.GradeInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class Grade : IGrade
    {
        public virtual int Id { get; set; }
        public virtual float Graded { get; set; }
        public virtual User User { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
