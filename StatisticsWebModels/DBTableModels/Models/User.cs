using StatisticsWebModels.DBTableModels.Interfaces.DepartmentInterfaces;
using StatisticsWebModels.DBTableModels.Interfaces.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual Department Department { get; set; }      
    }
}
