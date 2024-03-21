using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using backend.Interfaces;
using backend.Models;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"] ?? ""));
        }

        public string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName?? string.Empty)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetUsernamefromHeaderAuth(StringValues headerAuth)
        {
            string token = ExtractToken(headerAuth.First() ?? "");
            var jwtSecurity = ConvertJwtStringToJwtSecurityToken(token);
            return GetUsernameFromJwt(jwtSecurity);
        }

        public JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token;
        }

        public string GetUsernameFromJwt(JwtSecurityToken token)
        {
            var keyId = token.Header.Kid;
            var audience = token.Audiences.ToList();
            var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
            var givenName = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName).Value;

            return givenName;
        }

        public string ExtractToken(string headerAuth)
        {
            var jwtToken = headerAuth.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
            return jwtToken;
        }


    }
}
