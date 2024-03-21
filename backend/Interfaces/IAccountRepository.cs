using backend.Dtos.Account;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface IAccountRepository
    {
        Task<Users?> GetUserByUsernameAsync(string username);
        Task<Users?> GetUserByIdAsync(string id);
        Task<Users> CreateNewUserAsync(RegisterDto data);
        Task<Users> LoginUser(LoginDto data);
        Task<UserRole> GetRoleAsync(Users user);

    }
}
