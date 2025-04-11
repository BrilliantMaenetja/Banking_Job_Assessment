using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Extension.Models
{
    public record AuthenticationToken(string AccesToken, DateTime ExpirationDate, string RefreshToken, string[] Scopes)
    {
        public AuthenticationToken(string token, DateTime expirationDate, string refreshToken) : this(token, expirationDate, refreshToken, Array.Empty<string>())
        {
        }
    }
}