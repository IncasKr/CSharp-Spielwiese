using System;

namespace TeamAgile.NUnitExtensions.EventsTesting.SimpleTestsAsProof
{
	public class ThrowerUnderTest
	{
		public delegate void SomethingHappenedDelegate();
		public event SomethingHappenedDelegate SomethingHappened;

		public void OnSomethingHappened(bool shouldThrowEvent)
		{
			if(shouldThrowEvent)
			{
				SomethingHappened();
			}
		}

	}
}
