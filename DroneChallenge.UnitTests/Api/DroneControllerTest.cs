using DroneChallange.Api;
using DroneChallange.Application.Interfaces;
using Moq;
using Xunit;

namespace DroneChallange.UnitTests.Api
{
	public class DroneControllerTest : AbstractTest
	{
		[Fact]
		public async void Should_Return_WhenPost()
		{
			var controller = Mocker.CreateInstance<DroneController>();

			Mocker.GetMock<IDroneUseCase>().Setup(x => x.ProcessDeliveryService(It.IsAny<int>())).ReturnsAsync(chatMock);

			var result = await controller.ProcessDeliveryService();

			Assert.NotNull(result);
			Assert.True(result is IEnumerable<ChatResponse>);
		}
	}
}