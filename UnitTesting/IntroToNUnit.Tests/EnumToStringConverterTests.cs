using NUnit.Framework;

namespace IntroToNUnit.Tests
{
    public enum TestTypes
    {
        none = 0,
        a,
        B,
        Testing,
        UnitTesting,
        integrationTesting
    }

    [TestFixture]
    public class EnumToStringConverterTests
    {
        [TestCase((TestTypes)10, "")]
        [TestCase(TestTypes.a, "A")]
        [TestCase(TestTypes.B, "B")]
        [TestCase(TestTypes.none, "None")]
        [TestCase(TestTypes.Testing, "Testing")]
        [TestCase(TestTypes.UnitTesting, "Unit Testing")]
        [TestCase(TestTypes.integrationTesting, "Integration Testing")]
        public void CanConvertEnumIntoFriendlyString(TestTypes value, string expected)
        {
            // Arrange/Act
            var actual = value.ToFriendlyString();

            // Assert
            Assert.That(actual, Is.Not.Null.And.EqualTo(expected));
        }

        [TestCase((TestTypes)10, ExpectedResult = "")]
        [TestCase(TestTypes.a, ExpectedResult = "A")]
        [TestCase(TestTypes.B, ExpectedResult = "B")]
        [TestCase(TestTypes.none, ExpectedResult = "None")]
        [TestCase(TestTypes.Testing, ExpectedResult = "Testing")]
        [TestCase(TestTypes.UnitTesting, ExpectedResult = "Unit Testing")]
        [TestCase(TestTypes.integrationTesting, ExpectedResult = "Integration Testing")]
        public string CanConvertEnumIntoFriendlyString(TestTypes value)
        {
            return value.ToFriendlyString();
        }
    }
}
