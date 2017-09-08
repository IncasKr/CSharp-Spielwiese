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
        public static LogAnalyzer MakeDefaultAnalyzer()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.Initialize();
            return analyzer;
        }

        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            LogAnalyzer la = MakeDefaultAnalyzer();
            Assert.Throws(typeof(TypeLoadException), () => la.Analyze("myemptyfile.txt"));
        }

        [Test]
        public void SemanticsChange()
        {
            LogAnalyzer logan = MakeDefaultAnalyzer();
            Assert.IsFalse(logan.IsValid("abc"));
        }

        [Test]
        public void TestWithMultipleAsserts()
        {
            LogAnalyzer logan = MakeDefaultAnalyzer();
            Assert.IsFalse(logan.IsValid("abc"));
            Assert.IsTrue(logan.IsValid("abcde.txt"));
        }
    }
}
