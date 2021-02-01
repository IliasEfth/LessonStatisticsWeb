using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.DBTableModels.Converters.GradeInterfaces
{
    interface IGrade
    {
        float Graded { get; set; }
        Lesson Lesson { get; set; }
    }
}
