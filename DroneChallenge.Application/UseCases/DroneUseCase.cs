using DroneChallenge.Application.Interfaces;
using DroneChallenge.Domain;
using DroneChallenge.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace DroneChallenge.Application.UseCases
{
    public class DroneUseCase : IDroneUseCase
    {
        private readonly IFileService _fileService;
		private readonly ILogger<IDroneUseCase> _logger;

		public DroneUseCase(IFileService fileService, ILogger<IDroneUseCase> logger)
        {
            _fileService = fileService;
            _logger = logger;
		}

        public async Task<ResultDeliveryService> ProcessDeliveryService()
        {
			var result = new ResultDeliveryService();

			try
            {
                var inputFileContent = await _fileService.ReadDroneInputFile();

				if (inputFileContent == null || 
					inputFileContent.Drones == null || 
					!inputFileContent.Drones.Any() ||
					inputFileContent.Locations == null ||
					!inputFileContent.Locations.Any())
				{
					string message = "No file content or there are some errors to read the file.";
					_logger.LogError(message);
					result.Message = message;
					result.Success = false;

					return result;
				}

				if (inputFileContent.Drones.Count > 100)
				{
					string message = "The maximum number of drones in a squad is 100.";
					_logger.LogError(message);
					result.Message = message;
					result.Success = false;

					return result;
				}

				var drones = inputFileContent.Drones;
                var locationsToDeliver = inputFileContent.Locations.OrderBy(x => x.PackageWeight).ToList();

                while (locationsToDeliver.Any())
                {
                    CalculateTrips(drones, locationsToDeliver);

					result.Drones.AddRange(FindBestDroneToTrip(drones, locationsToDeliver));
				}

				await _fileService.WriteDroneOutputFile(result.Drones.OrderBy(x => x.Name).ToList());

				result.Success = true;
				return result;
			}
            catch(Exception ex) 
            {
                _logger.LogError("We found an error.", ex);
				return result;
			}
		}

		private List<Drone> FindBestDroneToTrip(List<Drone> drones, List<Location> locationsToDeliver)
		{
			var result = new List<Drone>();

			var bestDroneToThisTrip = drones.Where(x => x.Trips.Any()).OrderBy(x => x.TripsCount).ThenByDescending(x => x.Trips.Count()).FirstOrDefault();

			if (bestDroneToThisTrip != null)
			{
				bestDroneToThisTrip.TripsCount++;
				result.Add(new Drone { Name = bestDroneToThisTrip.Name, Trips = bestDroneToThisTrip.Trips });
			}

			foreach (var drone in result)
			{
				foreach (Location trip in drone.Trips)
					locationsToDeliver.Remove(trip);
			}

			drones = drones.Select(x => { x.Trips = new List<Location>(); x.LeftLoadWeight = x.Weight; return x; }).ToList();

			return result;
		}

		private void CalculateTrips(List<Drone> drones, List<Location> locationsToDeliver)
		{
			foreach (Drone drone in drones)
			{
				foreach (Location location in locationsToDeliver)
				{
					if (location.PackageWeight > drone.LeftLoadWeight)
						continue;
					else
					{
						drone.Trips.Add(location);
						drone.LeftLoadWeight = drone.LeftLoadWeight - location.PackageWeight;
					}
				}
			}
		}
	}
}