using backend.Dtos.Blogs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace backend.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IToken _tokenServices;
        private readonly IAccountRepository _accountRepository;

        public BlogController(IBlogRepository blogRepository, IToken tokenSer, IAccountRepository accountRepository)
        {
            _blogRepository = blogRepository;
            _tokenServices = tokenSer;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBlog(int id)
        {
            Log.Information(Request.Headers.Authorization);
            var res = await _blogRepository.GetBlogByIdAsync(id);
            if (res == null) return NotFound("Blog: " + id + " not found!");
            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBlogs([FromBody] string? uid)
        {
            var res = await _blogRepository.GetAllBlogsAsync(uid);
            Console.WriteLine(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogDto req)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = _tokenServices.GetUsernamefromHeaderAuth(Request.Headers.Authorization);
            var owner = await _accountRepository.GetUserByUsernameAsync(username);

            if (owner?.Id == null)
                return BadRequest("Cannot found owner-id");

            var res = await _blogRepository.UpdateOrCreateBlogAsync(req, owner.Id);
            return Ok(res);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var username = _tokenServices.GetUsernamefromHeaderAuth(Request.Headers.Authorization) ?? string.Empty;
            var owner = await _accountRepository.GetUserByUsernameAsync(username);

            if (owner?.Id == null)
                return BadRequest("Cannot found owner-id");

            var res = await _blogRepository.DeleteBlogAsync(id: id, owner_id: owner.Id);
            if (res == null)
                return NotFound("Blog: " + id + " not found!");

            return Ok(res);
        }


    }
}
