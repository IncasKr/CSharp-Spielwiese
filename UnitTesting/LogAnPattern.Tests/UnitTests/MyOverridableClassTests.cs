using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LogAnPattern.Tests.UnitTests
{
    public class RollbackAttribute : Attribute//, ITestAction
    {
        private TransactionScope transaction;

        public void BeforeTest(/*TestDetails testDetails*/)
        {
            transaction = new TransactionScope();
        }

        public void AfterTest(/*TestDetails testDetails*/)
        {
            transaction.Dispose();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }

    [TestFixture, Rollback]
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

        [Test, Rollback]
        public void Write_And_Rollback_IN_DB()
        {

        }
    }
}
