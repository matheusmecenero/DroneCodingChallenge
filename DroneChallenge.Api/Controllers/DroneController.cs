using Microsoft.AspNetCore.Mvc;
using DroneChallenge.Application.Interfaces;

namespace DroneChallenge.Api
{
    [ApiController]
	[Route("[controller]")]
	public class DroneController : ControllerBase
	{
		private readonly ILogger<DroneController> _logger;
		private readonly IDroneUseCase _droneUseCase;

		public DroneController(ILogger<DroneController> logger, IDroneUseCase droneUseCase)
		{
			_logger = logger;
			_droneUseCase = droneUseCase;
		}
		
		[HttpPost]
		[Route("ProcessDeliveryService")]
		public async Task ProcessDeliveryService()
		{
			try
			{
				await _droneUseCase.ProcessDeliveryService();
			}
			catch(Exception ex)
			{
				_logger.LogError("We fuond an error to process the delivery service.", ex);
			}
		}
	}
}