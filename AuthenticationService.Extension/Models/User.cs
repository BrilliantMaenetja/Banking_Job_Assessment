using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Extension.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Email { get; set; }
        public string? AccountNumber { get; set; }
        public string? Password { get; set; }
    }
}
