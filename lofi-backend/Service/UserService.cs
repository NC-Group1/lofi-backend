using lofi_backend.Data_Models;
using lofi_backend.Repository;

namespace lofi_backend.Service
{
    public interface IUserService
    {
        public User GetUser(int id);
        public User CreateUser(User user);
        public User EditUser(User user);
        public User RemoveUser(int id);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetUser(int id)
        {
            try
            {
                return _repository.FetchUser(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                throw;
            }
        }

        public User CreateUser(User user)
        {
            try
            {
                var newUser = _repository.InsertUser(user);
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public User EditUser(User user)
        {
            try
            {
                var updatedUser = _repository.UpdateUser(user);
                return updatedUser;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                throw;
            }
        }
        public User RemoveUser(int id)
        {
            try
            {
                return _repository.DeleteUser(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
