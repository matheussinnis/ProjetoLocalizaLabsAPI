using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth
{
    public class TokenCreator : ITokenCreator
    {
        public static string GetSecret() =>
            Environment.GetEnvironmentVariable("JWT_SECRET") ?? "str0ng-s3cr3t-f0r-t3sting";

        public string Create(User user)
        {
            var expirationStr = Environment.GetEnvironmentVariable("JWT_EXPIRATION") ?? "2";
            var expiration = Convert.ToInt32(expirationStr);
            var key = Encoding.ASCII.GetBytes(GetSecret());
            var tokenManager = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Document),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(expiration),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenManager.CreateToken(tokenDescriptor);
            return tokenManager.WriteToken(token);
        }
    }
}
