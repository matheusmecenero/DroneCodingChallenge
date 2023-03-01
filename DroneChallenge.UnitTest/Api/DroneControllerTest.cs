using DroneChallenge.Api;
using DroneChallenge.Application.Interfaces;
using DroneChallenge.Domain;
using Moq;
using Xunit;

namespace DroneChallenge.UnitTest.Api
{
	public class DroneControllerTest : BaseTest
	{
		[Fact]
		public async void ProcessDeliveryService_ShouldProcess_WhenPost()
		{
			var controller = Mocker.CreateInstance<DroneController>();

			Mocker.GetMock<IDroneUseCase>().Setup(x => x.ProcessDeliveryService()).ReturnsAsync(new ResultDeliveryService());

			await controller.ProcessDeliveryService();

			Mocker.VerifyAll();
		}

		[Fact]
		public void Should_ReturnReturnException_WhenServiceProblems()
		{
			var controller = Mocker.CreateInstance<DroneController>();

			Assert.ThrowsAsync<ArgumentException>(() => controller.ProcessDeliveryService());
		}
	}
}