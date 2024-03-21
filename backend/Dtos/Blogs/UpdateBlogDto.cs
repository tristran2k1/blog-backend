using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Blogs
{
    public class UpdateBlogDto
    {

        public int? id { get; set; } 
        public string? title { get; set; } 
        public string? content { get; set; }     

    }
}
