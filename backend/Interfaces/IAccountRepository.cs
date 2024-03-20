using backend.Models;

namespace backend.Interfaces
{
    public interface IAccountRepository
    {
        Task<Users?> GetUserByUsernameAsync(string username);
        Task<Users?> GetUserByIdAsync(string id);
    }
}
