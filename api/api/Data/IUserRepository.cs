using api.Models;

namespace api.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUserByEmail(string email);
    }
}
