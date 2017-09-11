using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class MyOverridableClass
    {
        public Func<int, int> calculateMethod = delegate (int i)
        {
            return i * 2;
        };

        public void DoSomeAction(int input)
        {
            int result = calculateMethod(input);
            if (result == -1)
            {
                throw new ArithmeticException("input was invalid");
            }
            //do some other work
        }
    }
}
