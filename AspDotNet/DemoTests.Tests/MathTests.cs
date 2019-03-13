using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoTests.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Factor_WithNegativeValue_Return1()
        {
            Assert.AreEqual(1, MathHelper.Factor(-5));
        }

        [TestMethod]
        public void Factor_WithValue0_Return1()
        {
            Assert.AreEqual(1, MathHelper.Factor(0));
        }

        [TestMethod]
        public void Factor_WithValue3_Return6()
        {
            Assert.AreEqual(6, MathHelper.Factor(3));
        }

        [TestMethod]
        public void Factorielle_AvecValeur15_Retourne1307674368000()
        {
            Assert.AreEqual(1307674368000, MathHelper.Factor(15));
        }
    }
}
