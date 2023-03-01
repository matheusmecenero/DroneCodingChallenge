using DroneChallenge.Domain;
using DroneChallenge.Service.Interfaces;
using DroneChallenge.Service.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DroneChallenge.Service
{
	public class FileService : IFileService
	{
		private readonly ILogger<IFileService> _logger;
		private readonly string localFile = AppDomain.CurrentDomain.BaseDirectory;

		public FileService(ILogger<IFileService> logger)
		{
			_logger = logger;
		}

		public async Task<InputFile> ReadDroneInputFile()
		{
			var response = new InputFile();
			
			try
			{
				var file = await File.ReadAllLinesAsync($"{localFile}InputFile.txt");
				var fileReader = file.ToList();

				var dronesRead = new List<Drone>();
				var droneName = string.Empty;

				var dronesLineFile = fileReader.FirstOrDefault();

				if (dronesLineFile == null)
				{
					_logger.LogError("We can't find the input file.");
					return response;
				}

				foreach (string droneRead in dronesLineFile.Split(','))
				{
					if (string.IsNullOrEmpty(droneName))
						droneName = droneRead.Replace("[", "").Replace("]", "").Trim();
					else
					{
						var droneWeight = droneRead.Replace("[", "").Replace("]", "");
						dronesRead.Add(new Drone { Name = droneName, Weight = Convert.ToInt32(droneWeight), LeftLoadWeight = Convert.ToInt32(droneWeight) });
						droneName = string.Empty;
					}
				}

				response.Drones = dronesRead;

				fileReader.Remove(dronesLineFile);

				var locationsRead = new List<Location>();

				foreach (string locationRead in fileReader)
				{
					var location = locationRead.Split(',');

					var locationName = location[0].Replace("[", "").Replace("]", "");
					var packageWeight = location[1].Replace("[", "").Replace("]", "");

					locationsRead.Add(new Location { Name = locationName, PackageWeight = Convert.ToInt32(packageWeight) });
				}

				response.Locations = locationsRead;
			}
			catch(Exception ex) 
			{
				_logger.LogError("We fuond an error to read the input file.", ex);
			}
			return response;
		}

		public async Task WriteDroneOutputFile(List<Drone> dronesResult)
		{
			try
			{
				using (StreamWriter outputFileWriter = new StreamWriter($"{localFile}OutputFile.txt"))
				{
					var tripNumberDrone = 1;
					foreach (var drone in dronesResult)
					{
						await outputFileWriter.WriteLineAsync($"[{drone.Name}]");
						await outputFileWriter.WriteLineAsync($"Trip #{tripNumberDrone}");
						tripNumberDrone++;

						var droneTrips = new StringBuilder();
						foreach (var trip in drone.Trips)
						{
							droneTrips = droneTrips.Append($"[{trip.Name.ToString()}],");
						}

						await outputFileWriter.WriteLineAsync(droneTrips.ToString().Substring(0, droneTrips.Length - 1));
						tripNumberDrone = 1;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("We fuond an error to write the output file.", ex);
			}
		}
	}
}