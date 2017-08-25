using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Impl;
using Rhino.Mocks.Interfaces;
using System;
using TeamAgile.NUnitExtensions.EventsTesting;

namespace LogAn.Tests
{
    /// <summary>
    /// Summary description for LogAnalyzer2Tests
    /// </summary>
    [TestFixture]
    public class LogAnalyzer2Tests
    {
        private bool VerifyComplexMessage(ComplexTraceMessage msg)
        {
            return !(msg.InnerMessage.Severity < 50 && msg.InnerMessage.Message.Contains("a"));
        }

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
                LastCall.Constraints(Rhino.Mocks.Constraints.Is.Anything());
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
                resultGetter.GetSomeNumber("ac");
                LastCall.Return(1); // Forces method call to return value
                resultGetter.GetSomeNumber("a");
                LastCall.Return(2);
                resultGetter.GetSomeNumber("b");
                LastCall.Return(3);
            }
            int result = resultGetter.GetSomeNumber("b");
            Assert.AreEqual(3, result);
            int result2 = resultGetter.GetSomeNumber("ac");
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

        [Test]
        public void SimpleStringConstraints()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockService = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                mockService.LogError("ignored string");
                LastCall.Constraints(new Rhino.Mocks.Constraints.Contains("abc"));
            }
            mockService.LogError($"{Guid.NewGuid()} abc");
            mocks.VerifyAll();
        }

        [Test]
        public void ConstraintsAgainstObjectPropeties()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("", 1, ""));
                // Set constraints
                Or combined1 = new Or(Property.Value("Message", "expected msg"),
                                        Property.Value("Severity", 100));
                And combined2 = new And(combined1, Property.Value("Source", "Some Source"));
                // Checks property values
                LastCall.Constraints(combined2);
            }
            mockservice.LogError(new TraceMessage("", 100, "Some Source"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestingObjectPropertiesWithObjects()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("", 1, "Some Source"));
            }
            mockservice.LogError(new TraceMessage("", 1, "Some Source"));
            mocks.VerifyAll();
        }

        [Test]
        public void ComplexConstraintsWithCallbacks()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("", 0, ""));
                LastCall.Constraints(Rhino.Mocks.Constraints.Is.Matching<ComplexTraceMessage>(VerifyComplexMessage));
            }
        }

        [Test]
        public void VerifyAttachesToViewEvents()
        {
            MockRepository mocks = new MockRepository();
            IView viewMock = (IView)mocks.StrictMock(typeof(IView));
            using (mocks.Record())
            {
                // Records expected event registration
                viewMock.Load += null;
                LastCall.IgnoreArguments();
            }
            new Presenter(viewMock);
            mocks.VerifyAll();
        }

        [Test]
        public void TriggerAndVerifyRespondingToEvents()
        {
            MockRepository mocks = new MockRepository();
            // Uses stub for event triggering
            IView viewStub = mocks.Stub<IView>();
            // Uses mock to check log call
            IWebService serviceMock = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                serviceMock.LogInfo("view loaded");
            }
            new Presenter(viewStub, serviceMock);
            // Creates event raiser
            IEventRaiser eventer = EventRaiser.Create(viewStub, "Load");
            eventer.Raise(null, EventArgs.Empty); // Triggers event
            mocks.Verify(serviceMock);
        }

        [Test]
        public void EventFiringManual()
        {
            bool loadFired = false;
            SomeView view = new SomeView();
            view.Load += delegate
            {
                loadFired = true;
            };
            view.TriggerLoad(null, EventArgs.Empty);
            Assert.IsTrue(loadFired);
        }

        [Test]
        public void EventFiringWithEventsVerifier()
        {
            EventsVerifier verifier = new EventsVerifier();
            SomeView view = new SomeView();
            verifier.Expect(view, "Load", null, EventArgs.Empty);
            view.TriggerLoad(null, EventArgs.Empty);
            verifier.Verify();
        }

        [Test]
        public void CreateMock_WithReplayAll()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService =
            mockEngine.DynamicMock<IWebService>();
            using (mockEngine.Record())
            {
                simulatedService.LogError("Filename too short:abc.ext");
            }
            LogAnalyzer2 log = new LogAnalyzer2(simulatedService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            mockEngine.Verify(simulatedService);
        }

        [Test]
        public void CreateMock_WithReplayAll_AAA()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService =
            mockEngine.DynamicMock<IWebService>();
            LogAnalyzer2 log = new LogAnalyzer2(simulatedService);
            // Moves to act mode
            mockEngine.ReplayAll();
            log.Analyze("abc.ext");
            // Asserts using Rhino Mocks
            simulatedService.AssertWasCalled( svc => svc.LogError("Filename too short:abc.ext"));
        }
    }
}
