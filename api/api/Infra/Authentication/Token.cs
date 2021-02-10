using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Microsoft.IdentityModel.Tokens;
using api.Domain.Entities;
using api.Domain.Authentication;

namespace api.Infra.Authentication
{
	public class Token : IToken
	{
			public string GerarToken(User user)
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"appsettings.json")));
				var key = Encoding.ASCII.GetBytes(jAppSettings["JwtToken"].ToString());
				var expirationTime = Convert.ToInt32(jAppSettings["ExpirationTime"]);
				var tokenDescriptor = new SecurityTokenDescriptor()
				{
					Subject = new ClaimsIdentity(new Claim[]{
						new Claim(ClaimTypes.Name, user.Email),
						new Claim(ClaimTypes.Role, user.Role.ToString()),
					}),
					Expires = DateTime.UtcNow.AddHours(expirationTime),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
			}
}
}
