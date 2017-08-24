using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace LogAn.Tests
{
    /// <summary>
    /// Summary description for LogAnalyzer2Tests
    /// </summary>
    [TestFixture]
    public class LogAnalyzer2Tests
    {

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            MockService mockService = new MockService();
            LogAnalyzer2 log = new LogAnalyzer2(mockService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            Assert.AreEqual("Filename too short:abc.ext", mockService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            // Creates manually stub object
            StubService stubService = new StubService()
            {
                ToThrow = new Exception("fake exception")
            };
            // Creates manually mock object
            MockEmailService mockEmail = new MockEmailService();
            //we use setters instead of constructor parameters for easier coding
            LogAnalyzer2 log = new LogAnalyzer2()
            {
                Service = stubService,
                Email = mockEmail
            };
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            Assert.AreEqual("a", mockEmail.To);
            Assert.AreEqual("fake exception", mockEmail.Body);
            Assert.AreEqual("subject", mockEmail.Subject);
        }

        [Test]
        public void Analyze_TooShortFileName_ErrorLoggedToService()
        {
            // Creates dynamic mock object
            MockRepository mocks = new MockRepository();
            IWebService simulatedService = mocks.DynamicMock<IWebService>();
            // Sets expectation
            // each method call on the simulated object is recorded as an expectation
            using (mocks.Record())
            {
                simulatedService.LogError("Filename too short:abc.ext");
            }
            // Invokes LogAnalyzer
            LogAnalyzer2 log = new LogAnalyzer2(simulatedService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            // Asserts expectations have been met
            mocks.VerifyAll();
        }

        [Test]
        public void ReturnResultsFromMock()
        {
            MockRepository mocks = new MockRepository();
            IGetResults resultGetter = mocks.DynamicMock<IGetResults>();
            using (mocks.Record())
            {
                resultGetter.GetSomeNumber("a");
                LastCall.Return(1); // Forces method call to return value
                resultGetter.GetSomeNumber("a");
                LastCall.Return(2);
                resultGetter.GetSomeNumber("b");
                LastCall.Return(3);
            }
            int result = resultGetter.GetSomeNumber("b");
            Assert.AreEqual(3, result);
            int result2 = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(1, result2);
            int result3 = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(2, result3);
        }
    }
}
