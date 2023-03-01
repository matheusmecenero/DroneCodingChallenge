using DroneChallange.Domain;

namespace DroneChallange.Repository.Interfaces
{
    public interface IUserRepository
	{
		public Task<bool> Add(User quote);
		public Task<IEnumerable<User>> GetAll();
		public Task<User?> GetByUserName(string userName);
		public Task<User?> GetById(int id);
		public Task<bool> Login(User user);
	}
}