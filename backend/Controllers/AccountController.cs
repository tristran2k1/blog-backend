using api.Service;
using backend.Dtos.Account;
using backend.Interfaces;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IToken _tokenServices;
        private readonly IAccountRepository _accountServices;

        public AccountController(IToken tokenServices, IAccountRepository accountServices)
        {
            _tokenServices = tokenServices;
            _accountServices = accountServices;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var res = await _accountServices.CreateNewUserAsync(registerDto);
                return Ok(new AuthenResDto
                {
                    UserName = res.UserName ?? "",
                    Email = res.Email ?? "",
                    Token = _tokenServices.CreateToken(res)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = await _accountServices.LoginUser(loginDto);
                return Ok(
                    new AuthenResDto
                    {
                        UserName = res.UserName ?? "",
                        Email = res.Email ?? "",
                        Token = _tokenServices.CreateToken(res)
                    }
                );
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
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
