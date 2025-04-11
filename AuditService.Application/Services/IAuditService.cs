using AuditService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Application.Services
{
    public  interface IAuditService
    {
        Task<bool> LogAction(int invokerId, string action);
        Task<List<Audit>> GetActions(int invokerId);
    }
}
