using DroneChallenge.Domain;
using DroneChallenge.Service.Models;

namespace DroneChallenge.Service.Interfaces
{
    public interface IFileService
	{
		public Task<InputFile> ReadDroneInputFile();
		public Task WriteDroneOutputFile(List<Drone> dronesResult);
	}
}