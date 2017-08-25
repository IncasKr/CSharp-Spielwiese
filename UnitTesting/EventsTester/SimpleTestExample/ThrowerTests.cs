using System;
using NUnit.Framework;

namespace TeamAgile.NUnitExtensions.EventsTesting.SimpleTestsAsProof
{

	[TestFixture]
	public class ThrowerTests
	{
		[Test]
		public void Create()
		{
			ThrowerUnderTest test = new ThrowerUnderTest();
			Assert.IsNotNull(test);
		}

		[Test]
		public void OnSomethingHappened_NoEventThrown()
		{
			ThrowerUnderTest test = new ThrowerUnderTest();

			EventsVerifier verifier = new EventsVerifier();
			verifier.ExpectNoEvent(test,"SomethingHappened");
			test.OnSomethingHappened(false);
		}
		
		[Test]
		public void OnSomethingHappened_EventThrown()
		{
			ThrowerUnderTest test = new ThrowerUnderTest();

			EventsVerifier verifier = new EventsVerifier();
			verifier.Expect(test,"SomethingHappened");
			test.OnSomethingHappened(true);
			verifier.Verify();
		}
	}
}
