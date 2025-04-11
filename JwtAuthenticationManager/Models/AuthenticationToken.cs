using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Common.Models
{
    public class AuthenticationToken
    {
        public string? Token { get; set; }
        public int Expiration { get; set; }

        public AuthenticationToken(string token , int expiration)
        {
            Token = token;
            Expiration = expiration;
        }

    }
}
