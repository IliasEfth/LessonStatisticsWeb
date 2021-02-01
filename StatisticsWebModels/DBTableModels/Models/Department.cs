using StatisticsWebModels.DBTableModels.Converters.DepartmentInterfaces;
using System.Collections.Generic;

namespace StatisticsWebModels
{
    public class Department : IDepartment
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual University University { get; set; }
    }
}
