using LogAnPattern.Tests.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.UnitTests
{
    [TestFixture]
    public class StandardStringParserTests : BaseStringParserTests
    {
        /// <summary>
        /// Defines the parser factory method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IStringParser GetParser(string input)
        {
            // Overrides abstract factory method.
            return new StandardStringParser(input);
        }

        [Test]
        public void GetStringVersionFromHeader_DoubleDigit_Found()
        {
            //this test is specific to the StandardStringParser type
            string input = "header;version=11;\n";
            IStringParser parser = GetParser(input);
            string versionFromHeader = parser.GetTextVersionFromHeader();
            Assert.AreEqual("11", versionFromHeader);
        }
    }
}
