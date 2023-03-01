using DroneChallange.Domain;
using DroneChallange.Repository.Interfaces;

namespace DroneChallange.Repository
{
    public class UserRepository : IUserRepository
	{
		private readonly List<User> users;

		public UserRepository()
		{
			this.users ??= new List<User>();
		}

		public async Task<bool> Add(User user)
		{
			users.Add(user);
			return await Task.FromResult(true);
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await Task.FromResult(users);
		}

		public async Task<User?> GetByUserName(string userName)
		{
			return await Task.FromResult(users?.FirstOrDefault(p => p.UserName == userName));
		}

		public async Task<User?> GetById(int id)
		{
			return await Task.FromResult(users?.FirstOrDefault(p => p.Id == id));
		}

		public async Task<bool> Login(User user)
		{
			var userById = users.FirstOrDefault(p => p.Id == user.Id);

			if (userById is null)
				return await Task.FromResult(false);

			userById.IsLogged = true;
			return await Task.FromResult(true);
		}
	}
}