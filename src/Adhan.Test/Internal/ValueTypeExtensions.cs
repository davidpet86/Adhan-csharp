using System;
using System.Collections.Generic;
using System.Text;

namespace Adhan.Test.Internal
{
    public static class ValueTypeExtensions
    {
        public static bool IsBetween(this double value, double minimum, double maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsWithin(this double value, double difference, double of)
        {
            double min = of - difference;
            double max = of + difference;

            return value.IsBetween(min, max);
        }

        public static bool IsAtMost(this long value, long atMost)
        {
            return value <= atMost;
        }
    }
}
