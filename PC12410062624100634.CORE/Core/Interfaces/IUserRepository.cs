using PC12410062624100634.CORE.Core.Entities;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> SignIn(string email, string password);
        Task<int> Signup(User user);
    }
}