using LogAnPattern.Tests.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.UnitTests
{
    [TestFixture]
    public class IsolationsAntiPatterns
    {
        private LogAnalyzer logan;

        [Test]
        public void CreateAnalyzer_BadFileName_ReturnsFalse()
        {
            logan = new LogAnalyzer();
            logan.Initialize();
            LoggingFacility.Logger = new StubLogger();
            bool valid = logan.IsValid("abc");
            Assert.That(valid, Is.False);
        }

        [Test]
        public void CreateAnalyzer_GoodFileName_ReturnsTrue()
        {
            bool valid = logan.IsValid("abcdefg.txt");
            Assert.That(valid, Is.True);
        }
    }
}
