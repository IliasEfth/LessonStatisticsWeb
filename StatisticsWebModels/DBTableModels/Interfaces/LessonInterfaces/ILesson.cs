using StatisticsWebModels.DBTableModels.Interfaces.GradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.DBTableModels.Interfaces.LessonInterfaces
{
    interface ILesson
    {
        string Name { get; set; }
        string Semester { get; set; }
        ICollection<Grade> Grades { get; set; }
        float ECTS { get; set; }
    }
}
