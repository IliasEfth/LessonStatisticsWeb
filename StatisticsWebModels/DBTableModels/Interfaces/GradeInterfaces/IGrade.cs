using StatisticsWebModels.DBTableModels.Interfaces.LessonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.DBTableModels.Interfaces.GradeInterfaces
{
    interface IGrade
    {
        float Graded { get; set; }
        Lesson Lesson { get; set; }
    }
}
