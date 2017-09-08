using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class IISLogStringParser : BaseStringParser
    {
        public IISLogStringParser(string value)
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
