using Moq.AutoMock;

namespace DroneChallange.UnitTests
{
	public class AbstractTest
	{
		public readonly AutoMocker Mocker;

		public AbstractTest() 
		{
			Mocker = new AutoMocker();
		}		
	}
}
