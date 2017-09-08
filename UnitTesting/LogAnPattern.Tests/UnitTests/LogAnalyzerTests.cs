using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            la.Analyze("myemptyfile.txt");
            //rest of test
        }
    }
}
