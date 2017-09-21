using IntroToNUnit.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace IntroToNUnit.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void StringContainsReturnsTrueIfStringIsFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.True);
        }

        [Test]
        public void StringContainsReturnsFalseIfStringIsNotFound()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "lazy fox";

            bool found = str.Contains(substr, StringComparison.CurrentCulture);

            Assert.That(found, Is.False);
        }

        [Test]
        public void StringContainsCanIgnoreCase()
        {
            string str = "The quick brown fox jumps over the lazy dog.";
            string substr = "the quick brown fox";

            bool found = str.Contains(substr, StringComparison.CurrentCultureIgnoreCase);

            Assert.That(found, Is.True);
        }
    }
}
