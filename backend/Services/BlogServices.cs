using backend.Data;
using backend.Dtos.Blogs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend.Services
{
    public class BlogServices : IBlogRepository
    {

        private readonly DataContext _context;

        public BlogServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Blogs?> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Blogs>> GetAllBlogsAsync(string? owner_id)
        {
            if (owner_id != null)
            {
                return await _context.Blogs.Where(b => b.owner_id == owner_id).ToListAsync();
            }
            return await _context.Blogs.ToListAsync();

        }

        public async Task<Blogs?> UpdateOrCreateBlogAsync(UpdateBlogDto data, string owner_id)
        {

            if (data?.id == null)
            {
                var newBlog = new Blogs
                {
                    title = data?.title ?? string.Empty,
                    content = data?.content ?? string.Empty,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                    owner_id = owner_id
                };
                
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();

                return newBlog;
            }

            var oldBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == data.id && b.owner_id == owner_id);
            if (oldBlog == null)
            {
                return null;
            }

            oldBlog.title = data.title ?? oldBlog.title;
            oldBlog.content = data.content ?? oldBlog.content;
            oldBlog.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return oldBlog;
        }

        public async Task<Blogs?> DeleteBlogAsync(int id, string owner_id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && b.owner_id == owner_id);
            if (blog == null)
                return null;
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

       
    }
}
