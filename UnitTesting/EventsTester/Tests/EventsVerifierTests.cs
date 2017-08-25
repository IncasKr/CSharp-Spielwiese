//EventsVerifier Tests for NUnit by Roy Osherove.
//Team Agile Consulting, www.TeamAgile.com
//blog: www.iserializable.com
//for questions and comments: eventsTesting@TeamAgile.com
using NUnit.Framework;

namespace TeamAgile.NUnitExtensions.EventsTesting.Tests
{

	[TestFixture]
	public class EventsVerifierTests
	{

		EventsVerifier verifier = new EventsVerifier();

		[SetUp]
		public void Setup()
		{
			verifier = new EventsVerifier();
		}

		[Test]
		public void Create()
		{
			verifier = new EventsVerifier();
			Assert.IsNotNull(verifier);
		}

		[Test]
		public void Expect_SingleEvent()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent");
			t.ThrowIntEvent(1);
			verifier.Verify();
		}
		
		
		[Test]
		public void Expect_TwoEvents_okWhenBothThrown()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent");
			verifier.Expect(t,"int2Event");

			t.ThrowIntEvent(1);
			t.ThrowInt2Event(2,1);
			verifier.Verify();
		}
		
		[Test]
		public void Expect_TwoEvents_okWhenBothThrownWithParams()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent",1);
			verifier.Expect(t,"int2Event",1,2);
			
			t.ThrowIntEvent(1);
			t.ThrowInt2Event(1,2);
			
			verifier.Verify();
		}
		
		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void Expect_TwoEvents_FailWhenBadParmasInOne()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent",1);
			verifier.Expect(t,"int2Event",1,1);
			t.ThrowIntEvent(1);
			t.ThrowInt2Event(2,1);
			verifier.Verify();
		}
		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void Expect_SingleEvent_FailsWhenNotThrown()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent");
			verifier.Verify();
		}
		
		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void Expect_TwoEvents_FailsWhenOneNotThrown()
		{
			EventRaiser t = new EventRaiser();
			verifier.Expect(t,"intEvent");
			verifier.Expect(t,"int2Event");
			t.ThrowIntEvent(1);
			verifier.Verify();
		}
		
	}

}
