using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebModels.DBTableModels.Converters.UserInterfaces
{
    interface IUser
    {
        int Id { get; set; }
        Department Department { get; set; }
    }
}
