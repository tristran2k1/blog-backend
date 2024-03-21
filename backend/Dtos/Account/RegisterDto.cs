using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
