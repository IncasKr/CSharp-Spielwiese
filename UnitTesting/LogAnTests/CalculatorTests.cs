using NUnit.Framework;

namespace LogAn.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calc = null;

        [SetUp]
        public void Setup()
        {
            _calc = new Calculator();
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void Sum_NoAddCalls_DefaultsToZero()
        {
            int lastSum = _calc.Sum();
            Assert.AreEqual(0, lastSum);
        }

        [Test]
        public void Add_CalledOnce_SavesNumberForSum()
        {
            _calc.Add(1);
            int lastSum = _calc.Sum();
            Assert.AreEqual(1, lastSum);
        }
    }
}
