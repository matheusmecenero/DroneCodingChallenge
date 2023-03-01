using DroneChallenge.Application.UseCases;
using DroneChallenge.Domain;
using DroneChallenge.Service.Interfaces;
using DroneChallenge.Service.Models;
using Moq;
using Xunit;

namespace DroneChallenge.UnitTest.Application
{
	public class DroneUseCaseTest : BaseTest
	{
		#region Use Case Tests
		[Fact]
		public async void ProcessDeliveryService_ShouldProcess_WhenPost()
		{
			var fileServiceMock = Mocker.GetMock<IFileService>();

			var mockInputFile = GetInputMocks(100, 3);

			fileServiceMock.Setup(x => x.ReadDroneInputFile()).ReturnsAsync(mockInputFile);

			var useCase = Mocker.CreateInstance<DroneUseCase>();

			var result = await useCase.ProcessDeliveryService();

			Mocker.VerifyAll();

			Assert.NotNull(result);
			Assert.True(result.Success);
			Assert.Null(result.Message);
			Assert.True(result.Drones.Any());
			Assert.Equal(result.Drones.Sum(x => x.Trips.Count), mockInputFile.Locations.Count);
		}

		[Fact]
		public async void ProcessDeliveryService_ShouldReturnError_WhenMoreThan100Drones()
		{
			var fileServiceMock = Mocker.GetMock<IFileService>();

			var mockInputFile = GetInputMocks(101, 1000);

			fileServiceMock.Setup(x => x.ReadDroneInputFile()).ReturnsAsync(mockInputFile);

			var useCase = Mocker.CreateInstance<DroneUseCase>();

			var result = await useCase.ProcessDeliveryService();

			Mocker.VerifyAll();

			Assert.NotNull(result);
			Assert.False(result.Success);
			Assert.Equal("The maximum number of drones in a squad is 100.", result.Message);
		}

		[Fact]
		public void Should_ReturnReturnException_WhenServiceProblems()
		{
			var useCase = Mocker.CreateInstance<DroneUseCase>();

			Assert.ThrowsAsync<ArgumentException>(() => useCase.ProcessDeliveryService());
		}

		#endregion

		#region Private Methods
		private InputFile GetInputMocks(int numberOfDrones, int numberOfLocations)
		{
			var locationsMock = new List<Location>();
			var dronesMock = new List<Drone>();

			while (numberOfLocations > 0)
			{
				locationsMock.Add(new Location
				{
					Name = $"Location{numberOfLocations}",
					PackageWeight = numberOfLocations * 3
				});
				numberOfLocations--;
			}

			while (numberOfDrones > 0)
			{
				dronesMock.Add(new Drone
				{
					Name = $"Drone{numberOfDrones}",
					Weight = numberOfDrones*10,
					LeftLoadWeight = numberOfDrones * 10
				});
				numberOfDrones--;
			}

			var inputFileMock = new InputFile
			{
				Drones = dronesMock,
				Locations = locationsMock
			};

			return inputFileMock;
		}
		#endregion
	}
}