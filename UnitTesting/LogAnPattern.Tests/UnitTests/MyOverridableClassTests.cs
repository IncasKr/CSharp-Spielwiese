using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.UnitTests
{
    [TestFixture]
    public class MyOverridableClassTests
    {
        [Test]
        public void DoSomething_GivenInvalidInput_ThrowsException()
        {
            MyOverridableClass c = new MyOverridableClass();
            int SOME_NUMBER = 1;
            //stub the calculation method to return "invalid"
            c.calculateMethod = delegate (int i) { return -1; };
            Assert.Throws(typeof(ArithmeticException), ()=> { c.DoSomeAction(SOME_NUMBER); });
        }
    }
}
