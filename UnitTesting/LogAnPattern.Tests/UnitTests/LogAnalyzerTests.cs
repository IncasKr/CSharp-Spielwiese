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
        private LogAnalyzer logan = null;

        [SetUp]
        public override void Setup()
        {
            logan = new LogAnalyzer();
            logan.Initialize();
        }
        
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            Assert.Throws(typeof(TypeLoadException), () => logan.Analyze("myemptyfile.txt"));
        }

        [Test]
        public void SemanticsChange()
        {
            Assert.IsFalse(logan.IsValid("abc"));
        }

        [Test]
        public void TestWithMultipleAsserts()
        {
            Assert.IsFalse(logan.IsValid("abc"));
            Assert.IsTrue(logan.IsValid("abcde.txt"));
        }
    }
}
