using api.Service;
using backend.Dtos.Account;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace backend.Services
{
    public class AccountServices : IAccountRepository
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountServices(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<Users> CreateNewUserAsync(RegisterDto data)
        {

            var newUser = new Users
            {
                UserName = data.Username,
                Email = data.Username,
            };
            var createdUser = await _userManager.CreateAsync(newUser, data.Password ?? "");
            if (createdUser.Succeeded)
            {
                string role = string.IsNullOrEmpty(data.Role) ? UserRole.USER.GetRoleName() : UserRole.ADMIN.GetRoleName();
                var roleResult = await _userManager.AddToRoleAsync(newUser, role);
                if (roleResult.Succeeded)
                {
                    return newUser;
                }
                throw new Exception(roleResult.Errors.FirstOrDefault()?.Description);

            }
            throw new Exception(createdUser.Errors.FirstOrDefault()?.Description);

        }

        public async Task<Users> LoginUser(LoginDto data)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == data.Username);
            if (user == null)
                throw new Exception("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

            if (!result.Succeeded)
                throw new Exception("Username not found and/or password incorrect");

            return user;
        }

        public async Task<UserRole> GetRoleAsync(Users user)
        {
            var userRole = await _userManager.GetRolesAsync(user);
            return ConvertEnum.GetRole(userRole.FirstOrDefault());
        }
    }
}
