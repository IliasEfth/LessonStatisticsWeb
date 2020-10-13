using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual Department Department { get; set; }
    }
}
