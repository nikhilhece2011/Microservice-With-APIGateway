using Identity.API.Infrastructure.Entities;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class JWTService : IJWTService
    {
        private readonly IOptions<GlobalIdentitySettings> _settings;
        public JWTService(IOptions<GlobalIdentitySettings> settings)
        {
            _settings = settings;
        }
        public object GetAccessToken(Users loginResult)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.SIGNING_KEY));

            var claims = new Claim[]
               {
                    new Claim("ID", loginResult.ID.ToString()),
                    new Claim("USERNAME", loginResult.UserName),
               };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Value.SIGNING_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _settings.Value.ISSUER,
                Audience = _settings.Value.AUDIENCE
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedJwt = tokenHandler.WriteToken(token);
            return new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(5).TotalSeconds
            };
        }
    }
}
