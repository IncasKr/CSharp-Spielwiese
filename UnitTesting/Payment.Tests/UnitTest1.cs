using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections;

namespace Payment.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GenerateMock_TakePaymentViaPaymentProcessorUsingMockService()
        {
            //#1 Arrange
            IPaymentProcessing mockProxy = MockRepository.GenerateMock<IPaymentProcessing>();
            //#2 Act
            mockProxy.Expect(x => x.TakePayment(1, 1, 10.0))
                    .Constraints(
                        Rhino.Mocks.Constraints.Is.Equal(1), 
                        Rhino.Mocks.Constraints.Is.Equal(1), 
                        Rhino.Mocks.Constraints.Is.Equal(10.0))
                    .Return(true);

             PaymentProcessor pp = new PaymentProcessor(mockProxy);
            bool result = pp.TakePayment(1, 1, 10.0);           
            Assert.IsTrue(result); // can be ignored
            
            //#3 Assert
            mockProxy.VerifyAllExpectations();
        }

        [Test]
        public void GenerateStub_TakePaymentViaPaymentProcessorUsingMockService()
        {
            //#1  Generate the stub
            IPaymentProcessing stubProxy = MockRepository.GenerateStub<IPaymentProcessing>();
            //#2  Define the stub
            stubProxy.Stub(action => action.TakePayment(1, 1, 10.0)).Return(true);                           

            PaymentProcessor pp = new PaymentProcessor(stubProxy);
            bool result = pp.TakePayment(1, 1, 10.0);
            Assert.IsTrue(result);
        }

        [Test]
        public void GenerateStub_AssertWasCalled_TakePaymentViaPaymentProcessorUsingMockService()
        {
            IPaymentProcessing stubProxy = MockRepository.GenerateStub<IPaymentProcessing>();
            stubProxy.Stub(action => action.TakePayment(1, 1, 10.0)).Return(true);

            PaymentProcessor pp = new PaymentProcessor(stubProxy);
            bool result = pp.TakePayment(1, 1, 10.0);
            Assert.IsTrue(result);

            stubProxy.AssertWasCalled(x => x.TakePayment(1, 1, 10.00));
        }

        [Test]
        public void Record_Playback_Test()
        {
            // Prepare mock repository
            MockRepository mocks = new MockRepository();
            IPaymentProcessing dependency = mocks.StrictMock<IPaymentProcessing>();

            // Record expectations
            using (mocks.Record())
            {
                Expect
                    .Call(dependency.TakePayment(1, 1, 10.00))
                    .IgnoreArguments()
                    .Return(true);
                dependency.DoSomething();
            }

            // Replay and validate interaction
            bool result;
            using (mocks.Playback())
            {
                PaymentProcessor underTest = new PaymentProcessor(dependency);
                result = underTest.TakePayment(1, 1, 10.00);
            }

            // Post-interaction assertions
            Assert.IsTrue(result);
        }
        
        private string Formal(string first, string surname)
        {
            return $"{first} {surname}";
        }

        [Test]
        public void Do_test()
        {
            MockRepository mocks = new MockRepository();
            INameSource nameSource = mocks.StrictMock<INameSource>();

            Expect.Call(nameSource.CreateName(null, null)).IgnoreArguments().
                Do(new NameSourceDelegate(Formal));
            mocks.ReplayAll();

            string expected = "Hi, my name is Gladis ND";
            string actual = new Speaker("Gladis", "ND", nameSource).Introduce();

            Assert.AreEqual(expected, actual);
            //mocks.VerifyAllExpectations();
        }

        public void MethodThatSubscribeToEventBlah(IPaidEvents events)
        {
            events.Paid += new EventHandler(Events_Blah);
        }

        private void Events_Blah(object obj, EventArgs e)
        {
            Console.WriteLine($"Event {e} is occurred.");
        }

        [Test]
        public void VerifyingThatEventWasAttached()
        {
            MockRepository mocks = new MockRepository();
            IPaidEvents events = mocks.StrictMock<IPaidEvents>();
            With.Mocks(mocks).Expecting(delegate
            {
                events.Paid += new EventHandler(Events_Blah);
            })
           .Verify(delegate
           {
               MethodThatSubscribeToEventBlah(events);
           });
        }


        [Test]
        public void VerifyingThatEventWasAttached_AAA()
        {
            var events = MockRepository.GenerateMock<IPaidEvents>();
            // Assign event
            events.Paid += Events_Blah;
            // verify event
            events.AssertWasCalled(x => x.Paid += Arg<EventHandler>.Is.Anything);
        }

        [Test]
        public void VerifyingThatAnEventWasFired()
        {
            MockRepository mocks = new MockRepository();
            IEventSubscriber subscriber = mocks.StrictMock<IEventSubscriber>();
            IPaidEvents events = new PaidEvents();
            // This doesn't create an expectation because no method is called on subscriber!! 
            events.Paid += new EventHandler(subscriber.Handler);
            subscriber.Handler(events, EventArgs.Empty);
            mocks.ReplayAll();
            events.RaiseEvent();
            mocks.VerifyAll();
        }

        [Test]
        public void Test_Transaction_With_Standard_Syntax()
        {
            MockRepository mocks = new MockRepository();
            IPaymentProcessing dependency = mocks.StrictMock<IPaymentProcessing>();
            
            // define expectations
            Expect.Call(dependency.TakePayment(1, 1, 10.00))
                .IgnoreArguments()
                .Return(true);
            dependency.DoSomething();
            mocks.ReplayAll();

            // run test subject
            PaymentProcessor subject = new PaymentProcessor(dependency);
            subject.TakePayment(1, 1, 10.00);

            // verify expectations
            mocks.VerifyAll();
        }

        [Test]
        public void Test_Transaction_With_RecordReplay_Syntax()
        {
            MockRepository mocks = new MockRepository();
            IPaymentProcessing dependency = mocks.StrictMock<IPaymentProcessing>();

            using (mocks.Record())
            {
                Expect.Call(dependency.TakePayment(1, 1, 10.00))
                    .IgnoreArguments()
                    .Return(true);
                dependency.DoSomething();
            }

            using (mocks.Playback())
            {
                PaymentProcessor subject = new PaymentProcessor(dependency);
                subject.TakePayment(1, 1, 10.00);
            }
        }

        [Test]
        public void Test_Transaction_With_Fluent_Syntax()
        {
            MockRepository mocks = new MockRepository();
            IPaymentProcessing dependency = mocks.StrictMock<IPaymentProcessing>();

            With.Mocks(mocks).Expecting(() =>
            {
                Expect.Call(dependency.TakePayment(1, 1, 10.00))
                .IgnoreArguments()
                .Return(true);
                dependency.DoSomething();
            })
            .Verify(delegate
            {
                PaymentProcessor subject = new PaymentProcessor(dependency);
                subject.TakePayment(1, 1, 10.00);
            });
        }

        [Test]
        public void AbuseArrayList_UsingGenerateStub()
        {
            ArrayList list = MockRepository.GenerateStub<ArrayList>();

            // Evaluate the values from the mock
            Assert.AreEqual(0, list.Capacity);
        }

        [Test]
        public void AbuseArrayList_UsingStrictMockGenerics()
        {
            MockRepository mocks = new MockRepository();
            ArrayList list = mocks.StrictMock<ArrayList>();

            // Setup the expectation of a call on the mock
            Expect.Call(list.Capacity).Return(999);
            mocks.ReplayAll();

            // Evaluate the values from the mock
            Assert.AreEqual(999, list.Capacity);
            mocks.VerifyAll(); ;
        }
        
    }
}
