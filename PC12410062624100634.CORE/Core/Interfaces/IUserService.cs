using PC12410062624100634.CORE.Core.DTOs;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> SignIn(string email, string password);
        Task<int> Signup(UserCreateDTO userCreateDTO);
    }
}