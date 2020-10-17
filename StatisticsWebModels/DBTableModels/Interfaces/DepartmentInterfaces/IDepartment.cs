using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;

namespace StatisticsWebModels.DBTableModels.Interfaces.DepartmentInterfaces
{
    interface IDepartment
    {        
        ICollection<Lesson> Lesson { get; set; }
        ICollection<User> User { get; set; }
        University University { get; set; }
    }
}
