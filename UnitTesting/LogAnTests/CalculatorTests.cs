using NUnit.Framework;

namespace LogAn.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Sum_NoAddCalls_DefaultsToZero()
        {
            Calculator calc = new Calculator();
            int lastSum = calc.Sum();
            Assert.AreEqual(0, lastSum);
        }
    }
}
