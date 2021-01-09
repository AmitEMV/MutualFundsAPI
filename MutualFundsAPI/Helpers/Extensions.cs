using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsAPI.Helpers
{
    public static class Extensions
    {
        public static double GetDoubleValue(this object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return 0;

            return double.Parse(value.ToString());
        }
    }
}
