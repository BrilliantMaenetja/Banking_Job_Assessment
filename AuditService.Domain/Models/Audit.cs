using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Domain.Models
{

    public  class Audit
    {
        public int Id { get; set; }
        public Action Action { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int InvokerID { get; set; }  

    }

    public enum Action
    {
        AccountCreation,
        Withdrawal,
        AccountUpdate,
        Logon
    }

}
