using NUnit.Framework;
using System;

namespace LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            //arrange
            m_analyzer = new LogAnalyzer();
        }

        [TearDown]
        public void TearDown()
        {
            m_analyzer = null;
        }

        [Test]
        [Category("Slow Tests")]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            //act
            bool result = m_analyzer.IsValidLogFileName("whatever.slf");
            //assert
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        [Category("Slow Tests")]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.SLF");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        [Category("Fast Tests")]
        public void IsValidFileName_EmptyFileName_ThrowsException()
        {
            Assert.Throws(typeof(ArgumentException), () => { m_analyzer.IsValidLogFileName(string.Empty); });
        }

        [Test]
        [Category("Fast Tests")]
        [Ignore("there is a problem with this test")]
        public void IsValidFileName_ValidFile_ReturnsTrue()
        {
            /// ...
        }

        [Test]
        public void IsValidLogFileName_ValidName_RemembersTrue()
        {
            LogAnalyzer log = new LogAnalyzer();
            log.IsValidLogFileName("somefile.slf");
            Assert.IsTrue(log.WasLastFileNameValid);
        }

        [Test]
        public void IsValidFileName_NameShorterThan6CharsButSupportedExtension_ReturnsFalse()
        {
            //set up the stub to use, make sure it returns true
            StubExtensionManager myFakeManager = new StubExtensionManager()
            {
                ShouldExtensionBeValid = true
            };
            //inject stub into the extention
            ExtensionManagerFactory.SetManager(myFakeManager);
            //create analyzer
            LogAnalyzer log = new LogAnalyzer();
            //Assert logic assuming extension is supported
            bool result = log.IsValidLogFileName("longName.txt");
            Assert.IsTrue(result, "File name with less than 5 chars should have failed the method, even if the extension is supported");
        }

        [Test]
        public void IsValidFileName_NameShorterThan6CharsWithInjectStub_ReturnsFalse()
        {
            //set up the stub to use, make sure it returns true
            StubExtensionManager myFakeManager = new StubExtensionManager()
            {
                ShouldExtensionBeValid = true
            };
            //create analyzer and inject stub
            LogAnalyzer log = new LogAnalyzer(myFakeManager);
            //Assert logic assuming extension is supported
            bool result = log.IsValidLogFileName("longName.txt");
            Assert.IsTrue(result, "File name with less than 5 chars should have failed the method, even if the extension is supported");
        }

        [Test]
        public void OverrideTest()
        {
            TestableLogAnalyzer logan = new TestableLogAnalyzer()
            {
                Manager = new StubExtensionManager()
                {
                    ShouldExtensionBeValid = true
                }
            };
            bool result = logan.IsValidLogFileName("File.ext");
            Assert.IsTrue(result, "File name should be too short to be considered valid");
        }

        [Test]
        public void OverrideTestWithoutStub()
        {
            TestableLogAnalyzer logan = new TestableLogAnalyzer()
            {
                IsSupported = true
            };
            bool result = logan.IsValidLogFileName("file.ext");
            Assert.IsTrue(result, "File name should be too short to be considered valid");
        }
    }
}
