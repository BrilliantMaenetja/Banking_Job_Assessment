using JwtAuthenticationManager.Common.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Common.Services
{
    public class JwtTokenService
    {
        private readonly List<User> _users = new List<User>
            {
                new User { Username = "admin", Password = "aDm1n", Role = "Administrator" },
                new User { Username = "user", Password = "user123", Role = "User" }
            };

        public AuthenticationToken? GenerateAuthToken(LoginModel loginModel)
        {
            var user = _users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);
            if (user == null)
                return null;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecureAndLongerSecretKey@123456"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(15); // Token valid for 15 minutes

            var claims = new List<Claim>
                {
                    new Claim("name", user.Username!),
                    new Claim("role", user.Role!)
                };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7165",
                claims: claims,
                expires: expirationTimeStamp,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            Console.WriteLine($"Generated Token: {tokenString}");

            return new AuthenticationToken(tokenString, (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds);
        }
    }
}
