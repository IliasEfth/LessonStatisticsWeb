using StatisticsWebModels.DBTableModels.Converters.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class User : IUser
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual Department Department { get; set; }      
    }
}
