using System;
namespace IntroToNUnit.Extensions
{
    /// <summary>
    /// Simple string extensions for demonstrating testing.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determine if one string is contained within another using a <see cref="StringComparison"/>.
        /// </summary>
        /// <param name="target">The string to search</param>
        /// <param name="searchFor">The string to search for</param>
        /// <param name="comparisonType">The rules for the search</param>
        /// <returns>true if the search string is found within the given string</returns>
        public static bool Contains(this string target, string searchFor, StringComparison comparisonType)
        {
            return target.IndexOf(searchFor, comparisonType) >= 0;
        }
    }
}
