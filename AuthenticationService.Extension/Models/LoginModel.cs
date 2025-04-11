using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Extension.Models
{
    public record LoginModel( string Username , string Password , string Role , string[] Scopes);
}
