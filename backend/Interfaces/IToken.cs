using backend.Helpers;
using backend.Models;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Interfaces
{
    public interface IToken
    {
        string ExtractToken(string headerAuth);
        string CreateToken(Users user, UserRole role);
        JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt);
        string GetUsernameFromJwt(JwtSecurityToken token);
        string GetUsernamefromHeaderAuth(StringValues headerAuth);
    }
}
