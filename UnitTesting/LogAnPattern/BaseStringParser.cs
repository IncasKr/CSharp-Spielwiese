using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public abstract class BaseStringParser : IStringParser
    {
        public string StringToParse { get; set; }

        public BaseStringParser()
        {
            StringToParse = null;
        }

        public abstract string GetTextVersionFromHeader();

        public abstract bool HasCorrectHeader();
    }
}
