using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Users : IdentityUser
    {
        public ICollection<Blogs> blogs { get; set; } = new List<Blogs>();
    }
}
