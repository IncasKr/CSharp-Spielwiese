﻿using NUnit.Framework;
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
            MockRepository mocks = new MockRepository();
            // Creates automatically stub and mock objects
            IWebService stubService = mocks.Stub<IWebService>();
            IEmailService mockEmail = mocks.StrictMock<IEmailService>();
            // Sets expectation
            using (mocks.Record())
            {
                stubService.LogError("whatever");
                LastCall.IgnoreArguments();
                LastCall.Throw(new Exception("fake exception"));
                mockEmail.SendEmail("a", "subject", "fake exception");
            }
            //we use setters instead of constructor parameters for easier coding
            LogAnalyzer2 log = new LogAnalyzer2()
            {
                Service = stubService,
                Email = mockEmail
            };
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            mocks.VerifyAll();
        }

        [Test]
        public void Analyze_TooShortFileName_ErrorLoggedToService()
        {
            MockRepository mocks = new MockRepository();
            // Creates dynamic mock object
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

        [Test]
        public void ReturnResultsFromStub()
        {
            MockRepository mocks = new MockRepository();
            IGetResults resultGetter = mocks.Stub<IGetResults>();
            using (mocks.Record())
            {
                resultGetter.GetSomeNumber("a");
                LastCall.Return(1);
            }
            int result = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void StubNeverFailsTheTest()
        {
            MockRepository mocks = new MockRepository();
            IGetResults resultGetter = mocks.Stub<IGetResults>();
            using (mocks.Record())
            {
                // Specifies a rule, not an expectation
                resultGetter.GetSomeNumber("a");
                LastCall.Return(1);
            }
            resultGetter.GetSomeNumber("b");
            mocks.Verify(resultGetter); // Will never fail on stubs
        }

        [Test]
        public void StubSimulatingException()
        {
            MockRepository mocks = new MockRepository();
            IGetResults resultGetter = mocks.Stub<IGetResults>();
            /* For simple properties on stub objects, get and set properties are 
             * automatically implemented and can be used as if the stub were a real object.*/
            resultGetter.ID = 2;
            using (mocks.Record())
            {
                resultGetter.GetSomeNumber("A");
                LastCall.Throw(new OutOfMemoryException("The system is out of memory!"));                
            }
            try
            {
                resultGetter.GetSomeNumber("A");
            }
            catch (Exception e)
            {
                Assert.AreEqual("The system is out of memory!", e.Message);
            }
        }
    }
}
