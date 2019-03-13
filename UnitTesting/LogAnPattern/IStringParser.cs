using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public interface IStringParser
    {
        string StringToParse { get; set; }

        string GetTextVersionFromHeader();

        bool HasCorrectHeader();
    }
}
