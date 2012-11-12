using NSpec;

namespace PutBrains2Use.Test
{
	public class ConferenceTrackManagementSpec : nspec
	{
		// Morning session should be less than or equal to 3hrs.
		// Evening session should be greater than or equal to 3hrs and less than or equal to 4hrs.
		// 12:00 to 1:00 Lunch.

		private void Given_the_talks_with_durations()
		{
			xit ["Arrange all the talks with descending time duration"] = () =>
			{
				var conferenceTrackManager = TestInstance.ConferenceTrackManager;
				dynamic talks = TestInstance.Talks;
				dynamic orderedTalks = conferenceTrackManager.OrderTalks(talks);
			};
		}

		//

		private class TestInstance
		{
			public static dynamic ConferenceTrackManager = null;

			public static dynamic Talks = null;
		}
	}
}