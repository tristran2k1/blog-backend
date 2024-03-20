using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Account
{
    public class AuthenResDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
