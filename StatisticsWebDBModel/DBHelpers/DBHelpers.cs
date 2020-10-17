using StatisticsWebDBModel.DBRelation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWebDBModel.DBHelpers
{
    public class DBHelpers
    {
        public static T execute<T>(Func<StatisticsWebDB, T> lambda)
        {
            T value;
            using (var context = new StatisticsWebDB())
            {
                value = lambda(context);
            }
            return value;
        }
    }
}
