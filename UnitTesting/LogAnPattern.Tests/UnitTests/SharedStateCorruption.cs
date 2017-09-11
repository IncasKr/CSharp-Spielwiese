using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.UnitTests
{
    [TestFixture]
    public class SharedStateCorruption
    {
        // Defines sharedPerson state.
        Person person = new Person();

        [Test]
        public void CreateAnalyzer_GoodFileName_ReturnsTrue()
        {
            person.AddNumber("055-4556684(34)"); // Changes shared state.
            string found = person.FindPhoneStartingWith("055");
            Assert.AreEqual("055-4556684(34)", found);
        }

        [Test]
        public void FindPhoneStartingWith_NoNumbers_ReturnsNull()
        {
            string found = person.FindPhoneStartingWith("0"); // Reads shared state.
            Assert.IsNull(found);
        }
    }

    internal class Person
    {
        public string PhoneNumber { get; set; }

        public void AddNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string FindPhoneStartingWith(string startNumbers)
        {
            return PhoneNumber.StartsWith(startNumbers) ? PhoneNumber : null;
        }
    }
}
