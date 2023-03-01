using Moq.AutoMock;

namespace DroneChallenge.UnitTest
{
	public class BaseTest
	{
		public readonly AutoMocker Mocker;

		public BaseTest()
		{
			Mocker = new AutoMocker();
		}
	}
}