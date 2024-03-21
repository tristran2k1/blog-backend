using backend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Interfaces
{
    public interface IToken
    {
        string CreateToken(Users user);
        JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt);
        string GetUsernameFromJwt(JwtSecurityToken token);
        
    }
}
