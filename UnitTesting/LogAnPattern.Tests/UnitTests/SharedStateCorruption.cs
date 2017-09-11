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

        internal int Sum(int nb1, int nb2, int nb3)
        {
            return (nb1 + nb2 == 3) ? nb1 + nb2 : nb2 + nb3;
        }

        [SetUp]
        public void Setup()
        {
            person.PhoneNumber = "";
        }

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

        [TestCase(1001, 1, 2, ExpectedResult = 3, Category = "Pass")]
        [TestCase(1, 1001, 2, ExpectedResult = 3, Category = "No pass")]
        [TestCase(1, 2, 1001, ExpectedResult = 3, Category = "Pass")]
        public int SumTests(int x, int y, int z)
        {
            return Sum(x, y, z);
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
