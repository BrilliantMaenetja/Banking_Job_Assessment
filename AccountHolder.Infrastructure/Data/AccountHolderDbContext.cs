using AccountHolder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountHolder.Infrastructure.Data
{
    public class AccountHolderDbContext : DbContext
    {
        public AccountHolderDbContext(DbContextOptions<AccountHolderDbContext> options) : base(options)
        {
        }

        public DbSet<AccountHolderr> AccountHolders { get; set; }


    }
}
