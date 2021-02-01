using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels.DBTableModels.Converters.GradeInterfaces;

namespace StatisticsWebModels.DBTableModels.Converters.LessonInterfaces
{
    public class UpdateLesson
    {
        public virtual int Id { get; set; }
        public virtual UpdateGrade Grades { get; set; }
    }
}
