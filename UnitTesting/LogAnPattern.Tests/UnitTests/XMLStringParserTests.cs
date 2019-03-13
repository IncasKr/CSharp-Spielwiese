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
    public class XMLStringParserTests : BaseStringParserTests
    {
        protected override IStringParser GetParser(string input)
        {
            // Overrides abstract factory method.
            return new XMLStringParser(input);
        }
    }
}
