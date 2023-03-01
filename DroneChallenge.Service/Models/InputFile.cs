using DroneChallenge.Domain;

namespace DroneChallenge.Service.Models
{
	public class InputFile
	{
		public List<Drone> Drones = new List<Drone>();
		public List<Location> Locations = new List<Location>();
	}
}