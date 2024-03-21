using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Blogs")]
    public class Blogs
    {
        public string Id { get; set; }
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

        public string owner_id { get; set; } = string.Empty;
        public Users owner { get; set; } =  new Users();
    }
}
