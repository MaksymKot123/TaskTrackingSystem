using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.DAL;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtGenerator(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config
                .GetSection("AppSettings:Token").Value));
        }

        /// <summary>
        /// This method generates JWT token for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns>JWT token</returns>
        public string CreateToken(User user, string role)
        {
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var credentials = new SigningCredentials(_key, 
                SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
