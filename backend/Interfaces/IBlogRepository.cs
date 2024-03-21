using backend.Dtos.Blogs;
using backend.Models;

namespace backend.Interfaces
{
    public interface IBlogRepository
    {

        Task<Blogs?> GetBlogAsync(string id);
        Task<List<Blogs>> GetAllBlogsAsync(string? owner_id);
        Task<Blogs?> updateOrCreateBlogAsync(UpdateBlogDto data, string owner_id);
        Task<Blogs?> deleteBlog(string id, string owner_id);
    }
}
