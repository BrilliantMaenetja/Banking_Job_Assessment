using AuditService.Domain.Models;
using AuditService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Infrastructure.Repository
{
    public class AuditService
    {
        private readonly AuditDbContext _context;
        public AuditService(AuditDbContext context)
        {
            _context = context;
        }
        public async Task<bool> LogAction(int invokerId, string action)
        {
            var audit = new Audit
            {
                InvokerID = invokerId,
                Action = (Domain.Models.Action)Enum.Parse(typeof(Domain.Models.Action), action),
                CreatedOn = DateTime.UtcNow
            };
            await _context.Audits.AddAsync(audit);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<string>> GetActions(int invokerId)
        {
            return await _context.Audits
                .Where(a => a.InvokerID == invokerId)
                .Select(a => a.Action.ToString())
                .ToListAsync();
        }
    }
}
