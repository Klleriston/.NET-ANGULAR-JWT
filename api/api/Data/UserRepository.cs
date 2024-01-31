using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            return user;

        }
        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            return user;

        }
    }
}
