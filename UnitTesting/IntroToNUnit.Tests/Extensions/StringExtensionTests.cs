using IntroToNUnit.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace IntroToNUnit.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test, Category("Fast"), Order(2)]
        public void StringContainsReturnsTrueIfStringIsFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.True);
        }

        [Test, Category("Fast"), Order(1)]
        public void StringContainsReturnsFalseIfStringIsNotFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "lazy fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.False);
        }

        [Test, Category("Test"), Order(3), Ignore("For the test")]
        public void StringContainsCanIgnoreCase()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "the quick brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCultureIgnoreCase);

            Assert.That(found, Is.True);
        }
    }
}
