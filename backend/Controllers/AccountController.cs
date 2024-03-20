using api.Service;
using backend.Dtos.Account;
using backend.Interfaces;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IToken _tokenServices;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAccountRepository _accountServices;

        public AccountController(UserManager<Users> userManager, IToken tokenServices, 
            SignInManager<Users> signInManager, IAccountRepository accountServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _signInManager = signInManager;
            _accountServices = accountServices;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = new Users
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(newUser, registerDto.Password ?? "");

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new AuthenResDto
                            {
                                UserName = newUser.UserName ?? "",
                                Email = newUser.Email ?? "",
                                Token = _tokenServices.CreateToken(newUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username); 
            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new AuthenResDto
                {
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Token = _tokenServices.CreateToken(user)
                }
            );
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _accountServices.GetUserByUsernameAsync(username);
            if (user == null)
            {
               return NotFound("User not found");
            }
            return Ok(
                new AuthenResDto
                {
                    UserName = user?.UserName ?? "",
                    Email = user?.Email ?? "",
                    Token = _tokenServices.CreateToken(user)
                }
            );
        }


    }
}
