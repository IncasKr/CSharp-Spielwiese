using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class StandardStringParser : BaseStringParser
    {
        public StandardStringParser(string value)
        {
            StringToParse = value;
        }

        public override string GetTextVersionFromHeader()
        {
            var tabStrings = StringToParse.Split(';');
            if (tabStrings.Length < 3)
            {
                return null;
            }
            return tabStrings[1].Split('=')[1];
        }

        public override bool HasCorrectHeader()
        {
            var tabStrings = StringToParse.Split(';');
            return tabStrings.Length.Equals(3) 
                && tabStrings[0].Trim().ToLower().Equals("header") 
                && tabStrings[1].Split('=')[0].Trim().ToLower().Equals("version");
        }
    }
}
