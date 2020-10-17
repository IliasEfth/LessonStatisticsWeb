using StatisticsWebModels.DBTableModels.Interfaces.DepartmentInterfaces;
using System.Collections.Generic;

namespace StatisticsWebModels
{
    public class Department : IDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual University University { get; set; }
    }
}
