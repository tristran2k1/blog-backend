using backend.Data;
using backend.Dtos.Account;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AccountServices : IAccountRepository
    {
        private readonly UserManager<Users> _userManager;
        public AccountServices(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
