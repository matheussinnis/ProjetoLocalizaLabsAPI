using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Microsoft.IdentityModel.Tokens;
using api.Domain.Entities;

namespace api.Domain.Authentication
{
	public interface IToken
	{
    string GerarToken(User user);
  }
}