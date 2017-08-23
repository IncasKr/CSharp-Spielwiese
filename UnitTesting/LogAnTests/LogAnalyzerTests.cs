using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogAn;

namespace LogAnTests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [TestMethod]
        public void IsValidFileName_validFile_ReturnsTrue()
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
