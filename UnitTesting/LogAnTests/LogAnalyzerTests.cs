using NUnit.Framework;
using System;

namespace LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            //arrange
            m_analyzer = new LogAnalyzer();
        }

        [TearDown]
        public void TearDown()
        {
            m_analyzer = null;
        }

        [Test]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            //act
            bool result = m_analyzer.IsValidLogFileName("whatever.slf");
            //assert
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.SLF");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ThrowsException()
        {
            Assert.Throws(typeof(ArgumentException), () => { m_analyzer.IsValidLogFileName(string.Empty); });
        }
    }
}
