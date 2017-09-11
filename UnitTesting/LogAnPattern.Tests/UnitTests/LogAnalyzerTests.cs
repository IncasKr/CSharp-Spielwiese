using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using LogAnPattern.Tests.Integration;
using LogAnPattern.Tests.Base;
using System.IO;

namespace LogAnPattern.Tests.UnitTests
{
    /// <summary>
    /// Summary description for LogAnalyzerTests
    /// </summary>
    [TestFixture]
    public class LogAnalyzerTests : BaseTestClass
    {
        private FileInfo fileInfo = null;
        private LogAnalyzer logan = null;

        [SetUp]
        public override void Setup()
        {
            logan = new LogAnalyzer();
            logan.Initialize();
            fileInfo = new FileInfo("c:\\someFile.txt");
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
        public void IsValid_LengthBiggerThan8_IsFalse()
        {
            Assert.IsTrue(logan.IsValid("123456789.txt"));
        }

        [Test]
        public void IsValid_LengthSmallerThan8_IsTrue()
        {
            Assert.IsTrue(logan.IsValid("123.txt"));
        }

        [Test]
        public void IsValid_BadFileInfoInput_returnsFalse()
        {
            bool valid = logan.IsValid(fileInfo);
            Assert.IsFalse(valid);
        }
    }
}
