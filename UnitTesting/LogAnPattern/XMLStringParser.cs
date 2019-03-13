using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class XMLStringParser : BaseStringParser
    {
        public XMLStringParser(string value)
        {
            StringToParse = value;
        }

        public override string GetTextVersionFromHeader()
        {
            return null;
        }

        public override bool HasCorrectHeader()
        {
            return false;
        }
    }
}
