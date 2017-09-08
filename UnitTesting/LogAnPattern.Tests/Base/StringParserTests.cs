using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.Base
{
    /// <summary>
    /// Defines generic constraint on parameter
    /// </summary>
    /// <typeparam name="T">The class type which implementes the <see cref="IStringParser"/> interface.</typeparam>
    public abstract class StringParserTests<T> where T : IStringParser
    {
        /// <summary>
        /// Gets a parser of this data.
        /// </summary>
        /// <param name="input">Data</param>
        /// <returns>Returns generic type.</returns>
        protected T GetParser(string input)
        {
            return (T)Activator.CreateInstance(typeof(T), input);
        }

        [Test]
        public void HasCorrectHeader_NoSpaces_ReturnsTrue()
        {
            string input = "header; \n";
            T parser = GetParser(input);
            bool result = parser.HasCorrectHeader();
            Assert.IsFalse(result);
        }
    }
}
