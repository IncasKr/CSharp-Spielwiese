using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace IntroToNUnit
{
    /// <summary>
    /// Converts an enum to a friendly string by breaking apart words on capital letters.
    /// </summary>
    public static class EnumToStringConverter
    {
        public static string ToFriendlyString(this Enum value)
        {
            var text = value.ToString();
            var regex = new Regex(@"[\p{Lu}|^\p{Ll}]\p{Ll}*");
            var split = from Match match in regex.Matches(text) select match.Value.InitialCapital();
            return string.Join(" ", split);
        }

        public static string InitialCapital(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;

            return word.First().ToString().ToUpper() + word.Substring(1);
        }
    }
}
