using backend.Dtos.Blogs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IToken _tokenServices;
        public BlogController(IBlogRepository blogRepository, IToken tokenSer)
        {
            _blogRepository = blogRepository;
            _tokenServices = tokenSer;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetBlog(string id)
        {
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogDto req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (string.IsNullOrEmpty(req.id))
            {
                // create new


            }
            // update
            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            return Ok(id);
        }


    }
}
