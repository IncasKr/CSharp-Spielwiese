using NUnit.Framework;
using LogAnPattern.Tests.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.UnitTests
{
    /// <summary>
    /// Inherits from generic base class <see cref="StringParserTests"/>.
    /// </summary>
    [TestFixture]
    public class StandardStringParserGenericTests : StringParserTests<StandardStringParser>
    {
        [Test]
        public void GetStringVersionFromHeader_DoubleMinor_Found()
        {
            //this test is specific to the StandardStringParser type
            string input = "header;version=1.12;\n";
            IStringParser parser = GetParser(input);
            string versionFromHeader = parser.GetTextVersionFromHeader();
            Assert.AreEqual("1.12", versionFromHeader);
        }
    }
}
