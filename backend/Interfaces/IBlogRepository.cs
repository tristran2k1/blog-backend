using backend.Dtos.Blogs;
using backend.Models;

namespace backend.Interfaces
{
    public interface IBlogRepository
    {

        Task<Blogs?> GetBlogByIdAsync(int id);
        Task<List<Blogs>> GetAllBlogsAsync(string? owner_id);
        Task<Blogs?> UpdateOrCreateBlogAsync(UpdateBlogDto data, string owner_id);
        Task<Blogs?> DeleteBlogAsync(int id, string owner_id);
    }
}
