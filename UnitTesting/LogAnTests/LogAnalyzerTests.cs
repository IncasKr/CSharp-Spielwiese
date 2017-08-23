using LogAn;
using NUnit.Framework;

namespace LogAnTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            m_analyzer = new LogAnalyzer();
        }

        [Test]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            //arrange
            LogAnalyzer analyzer = new LogAnalyzer();
            //act
            bool result = analyzer.IsValidLogFileName("whatever.slf");
            //assert
            Assert.IsTrue(result, "filename should be valid!");
        }
    }
}
