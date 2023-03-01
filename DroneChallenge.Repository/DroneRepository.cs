using DroneChallange.Domain;
using DroneChallange.Repository.Interfaces;

namespace DroneChallange.Repository
{
    public class DroneRepository : IDroneRepository
	{
		private readonly List<Drone> drones;

		public DroneRepository()
		{
			this.drones = this.drones ?? new List<Drone>();
		}

		public async Task<bool> Add(Drone drone)
		{
			drones.Add(drone);
			return await Task.FromResult(true);
		}
	}
}