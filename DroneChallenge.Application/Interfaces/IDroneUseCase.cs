using DroneChallenge.Domain;

namespace DroneChallenge.Application.Interfaces
{
    public interface IDroneUseCase
	{
		public Task<ResultDeliveryService> ProcessDeliveryService();
	}
}