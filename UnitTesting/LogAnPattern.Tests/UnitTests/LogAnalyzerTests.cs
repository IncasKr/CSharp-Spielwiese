using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using LogAnPattern.Tests.Integration;
using LogAnPattern.Tests.Base;

namespace LogAnPattern.Tests.UnitTests
{
    /// <summary>
    /// Summary description for LogAnalyzerTests
    /// </summary>
    [TestFixture]
    public class LogAnalyzerTests : BaseTestClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            LogAnalyzer la = new LogAnalyzer();
            Assert.Throws(typeof(TypeLoadException), () => la.Analyze("myemptyfile.txt"));
        }

        [Test]
        public void SemanticsChange()
        {
            LogAnalyzer logan = new LogAnalyzer();
            logan.Initializer();
            Assert.IsFalse(logan.IsValid("abc"));
        }
    }
}
