using lofi_backend.Data_Models;
using lofi_backend.Database;
using Microsoft.EntityFrameworkCore;

namespace lofi_backend.Repository
{
    public interface IUserRepository
    {
        public User FetchUser(int id);
        public User InsertUser(User user);
        public User UpdateUser(User user);
        public User DeleteUser(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly LoFiDbContext _db;

        public UserRepository(LoFiDbContext dbContext)
        {
            _db = dbContext;
        }

        public User FetchUser(int id)
        {
            return _db.Users.ToList().First(u => u.Id == id) ?? throw new Exception("User not found");
        }

        public User InsertUser(User user)
        {
            if (_db.Users.Contains(user)) throw new Exception("User exists");

            var newUser = _db.Users.Add(user).Entity;
            _db.SaveChanges();

            return newUser;
        }

        public User UpdateUser(User user)
        {
            if (_db.Users.Contains(user)) throw new Exception("User exists");

            var updatedUser = _db.Users.Update(user).Entity;
            _db.SaveChanges();
            return updatedUser;
        }
        
        public User DeleteUser(int id)
        {
            var deletedUser = _db.Users.First(u => u.Id == id);

            if (deletedUser == null)
                throw new Exception("User does not exist");

            _db.Users.Remove(deletedUser);

            _db.SaveChanges();

            return deletedUser;

        }
    }
}
