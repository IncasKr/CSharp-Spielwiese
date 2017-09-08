using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class LogStringParser : BaseStringParser
    {
        public LogStringParser()
        {

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
