using StatisticsWebDBModel.DBRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebDBModel.DBHelpers
{
    public class DBHelpers
    {
        public static T execute<T>(Func<StatisticsWebDB, T> lambda)
        {
            T value;
            using (var context = new StatisticsWebDB())
            {
                value = lambda.Invoke(context);
            }
            return value;
        }
    }
}
