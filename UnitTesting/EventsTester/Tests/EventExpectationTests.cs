//EventsVerifier Tests for NUnit by Roy Osherove.
//Team Agile Consulting, www.TeamAgile.com
//blog: www.iserializable.com
//for questions and comments: eventsTesting@TeamAgile.com
using System;
using NUnit.Framework;

namespace TeamAgile.NUnitExtensions.EventsTesting.Tests
{
	[TestFixture]
	public class EventExpectationTests
	{
		private EventExpectation expectation = null;

		[SetUp]
		public void Setup()
		{
			expectation = makeExpectation();
		}

		[Test]
		public void Create()
		{
			Assert.IsNotNull(expectation);
		}

		private EventExpectation makeExpectation()
		{
			return new EventExpectation();
		}


		[Test]
		public void ExpectEvent_SuccessWhenCorrectSingleParam()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "SimpleEvent", null, EventArgs.Empty);
			t.ThrowSimpleEvent(null, EventArgs.Empty);

			expectation.Verify();
		}

		[Test]
		public void ExpectEvent_SuccessWhenCorrectSingleParam_using()
		{
			EventRaiser t = new EventRaiser();
			using (expectation)
			{
				expectation.ExpectEvent(t, "SimpleEvent", null, EventArgs.Empty);
				t.ThrowSimpleEvent(null, EventArgs.Empty);
			}
		}

		#region ExpectNoEvent

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectNoEvent_FailsIfEventRaised()
		{
			EventRaiser t = new EventRaiser();
			using (expectation)
			{
				expectation.ExpectNoFire(t, "SimpleEvent");
				t.ThrowSimpleEvent(null, EventArgs.Empty);
			}
		}

		[Test]
		public void ExpectNoEvent_OkIfNotRaised()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectNoFire(t, "SimpleEvent");

			expectation.Verify();
		}

		#endregion

		#region ExpectEvent

		[Test]
		public void ExpectEvent_SuccessWhenEventThrown()
		{
			EventRaiser2 t = new EventRaiser2();
			expectation.ExpectEvent(t, "SimpleEvent2");
			t.ThrowSimpleEvent2(null, EventArgs.Empty);

			expectation.Verify();
		}


		[Test]
		public void ExpectEvent_SuccessWhenCorrectTwoParam()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "int2Event", 1, 2);
			t.ThrowInt2Event(1, 2);

			expectation.Verify();
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectEvent_FailWhenIncorrect2ndParam()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "int2Event", 1, 2);
			t.ThrowInt2Event(1, 3);

			expectation.Verify();
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectEvent_FailWhenIncorrectSingleParam()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "intEvent", 1);
			t.ThrowIntEvent(2);

			expectation.Verify();
		}


		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectEvent_ExceptionWhenEventNotThrown()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "SimpleEvent");

			expectation.Verify();
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectEvent_ExceptionWhenEventNotThrown_CorrectEventName()
		{
			EventRaiser t = new EventRaiser();
			expectation.ExpectEvent(t, "SimpleEvent2");

			expectation.Verify();
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void ExpectEvent_ExceptionWhenEventNotThrown_CorrectClassName()
		{
			EventRaiser2 t = new EventRaiser2();
			expectation.ExpectEvent(t, "SimpleEvent");

			expectation.Verify();
		}

		#endregion
	}

	internal class EventRaiser
	{
		public delegate void IntEventDelegate(int i);

		public event IntEventDelegate intEvent;

		public void ThrowIntEvent(int i)
		{
			intEvent(i);
		}

		public delegate void Int2EventDelegate(int i, int j);

		public event Int2EventDelegate int2Event;

		public void ThrowInt2Event(int i, int j)
		{
			int2Event(i, j);
		}

		public event EventHandler SimpleEvent;
		public event EventHandler SimpleEvent2;

		public void ThrowSimpleEvent(object sender, EventArgs args)
		{
			SimpleEvent(sender, args);
		}

		public void ThrowSimpleEvent2(object sender, EventArgs args)
		{
			SimpleEvent2(sender, args);
		}
	}

	internal class EventRaiser2 : EventRaiser
	{
	}
}