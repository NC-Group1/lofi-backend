using lofi_backend.Data_Models;
using lofi_backend.Repository;
using lofi_backend.Repository.Authentication;
using Supabase.Gotrue;

namespace lofi_backend.Service
{
    public interface IUserService
    {
        public Task<AuthenticatedUser> GetUserAsync(string username, string password);
        public Task<AuthenticatedUser> CreateUser(UserWithPassword user);
        public UserData EditUser(UserData user);
        public UserData RemoveUser(string id);
        List<UserData> GetAllUsers();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationRepository _authRepository;

        public UserService(IUserRepository repository, IAuthenticationRepository authRepository)
        {
            _repository = repository;
            _authRepository = authRepository;
        }

        public List<UserData> GetAllUsers()
        {
            return _repository.FetchAllUser();
        }


        public async Task<AuthenticatedUser> GetUserAsync(string username, string password)
        {
            try
            {
                var userInDb = _repository.FetchUser(username);
                var authToken = await _authRepository.SignInAsync(userInDb.Email, password);
                return new AuthenticatedUser(userInDb, authToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                throw;
            }
        }

        public async Task<AuthenticatedUser> CreateUser(UserWithPassword user)
        {
            try
            {
                var authToken = await _authRepository.SignUpAsync(user.UserData.Email, user.Password);

                Console.WriteLine("new userId: " + user.UserData.Id);

                user.UserData.Id = authToken.Id;
                var newUser = _repository.InsertUser(user.UserData);

                return new AuthenticatedUser(newUser, authToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public UserData EditUser(UserData user)
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

        public UserData RemoveUser(string id)
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
