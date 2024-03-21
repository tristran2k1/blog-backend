using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Blogs
{
    public class UpdateBlogDto
    {

        public string id { get; set; } = string.Empty;
        public string? title { get; set; } 
        public string? content { get; set; }     

    }
}
