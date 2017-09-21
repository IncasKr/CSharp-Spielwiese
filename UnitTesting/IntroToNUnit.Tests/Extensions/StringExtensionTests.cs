using IntroToNUnit.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace IntroToNUnit.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test, Category("Fast"), Order(3), Description("String found, than returns true")]
        public void StringContainsReturnsTrueIfStringIsFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.True);
        }

        [Test, Category("Fast"), Order(2), Description("String not found, than returns false")]
        public void StringContainsReturnsFalseIfStringIsNotFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "lazy fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.False);
        }

        [Test, Category("Fast"), Order(1), Description("Ignore case of string"), Ignore("For test")]
        public void StringContainsCanIgnoreCase()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "the quick brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCultureIgnoreCase);

            Assert.That(found, Is.True);
        }
    }
}
