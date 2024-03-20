using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Blogs
{
    public class UpdateBlogDto
    {

        public string id { get; set; } = string.Empty;
        [Required]
        public string? title { get; set; } 
        [Required]
        public string? content { get; set; }     

    }
}
