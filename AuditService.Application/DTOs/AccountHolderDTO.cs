using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Application.DTOs
{
    public class AccountHolderDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "Id Number must not exceed 13 digits")]
        [MinLength(13, ErrorMessage = "Id Number must be 13 digits")]
        public string? IdentityNo { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? Action { get; set; }
    }
}
