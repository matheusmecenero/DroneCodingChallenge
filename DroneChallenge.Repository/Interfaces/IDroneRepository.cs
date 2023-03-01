using DroneChallange.Domain;

namespace DroneChallange.Repository.Interfaces
{
    public interface IDroneRepository
    {
        public Task<bool> Add(Drone drone);
	}
}