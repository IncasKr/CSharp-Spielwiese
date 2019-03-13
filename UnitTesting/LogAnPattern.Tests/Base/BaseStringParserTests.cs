using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.Base
{
    public abstract class BaseStringParserTests
    {
        /// <summary>
        /// Turns <see cref="GetParser"/> into an abstract method
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected abstract IStringParser GetParser(string input);

        [Test]
        public void GetStringVersionFromHeader_SingleDigit_Found()
        {
            string input = "header;version=1;\n";
            IStringParser parser = GetParser(input); // Calls abstract factory method.
            string versionFromHeader = parser.GetTextVersionFromHeader();
            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithMinorVersion_Found()
        {
            string input = "header;version=1.1;\n";
            IStringParser parser = GetParser(input);
            string versionFromHeader = parser.GetTextVersionFromHeader();
            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithRevision_Found()
        {
            string input = "header;version=1.1.1;\n";
            IStringParser parser = GetParser(input);
            string versionFromHeader = parser.GetTextVersionFromHeader();
            Assert.AreEqual("1.1.1", versionFromHeader);
        }

        [Test]
        public void HasCorrectHeader_NoSpaces_ReturnsTrue()
        {
            string input = "header;version=1.1.1;\n";
            IStringParser parser = GetParser(input);
            bool result = parser.HasCorrectHeader();
            Assert.IsTrue(result);
        }

        [Test]
        public void HasCorrectHeader_WithSpaces_ReturnsTrue()
        {
            string input = "header ; version=1.1.1 ; \n";
            IStringParser parser = GetParser(input);
            bool result = parser.HasCorrectHeader();
            Assert.IsTrue(result);
        }

        [Test]
        public void HasCorrectHeader_MissingVersion_ReturnsFalse()
        {
            string input = "header; \n";
            IStringParser parser = GetParser(input);
            bool result = parser.HasCorrectHeader();
            Assert.IsFalse(result);
        }
    }
}
