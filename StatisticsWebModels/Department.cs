using System.Collections.Generic;

namespace StatisticsWebModels
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual University University { get; set; }
    }
}
