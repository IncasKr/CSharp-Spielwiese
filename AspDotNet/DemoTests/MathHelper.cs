using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoTests
{
    public static class MathHelper
    {
        public static long Factor(int value)
        {
            if (value <= 1)
            {
                return 1;
            }
            return value * Factor(value - 1);
        }
    }
}