using AccountHolder.Application.Interfaces;
using AccountHolder.Domain.Models;
using AccountHolder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountHolder.Infrastructure.Services
{
    public class AccountHolderService : IAccountHolderService
    {

        private readonly AccountHolderDbContext _context;

        public AccountHolderService(AccountHolderDbContext context)
        {
            _context = context;
        }

        public async Task<AccountHolderr> CreateAccountHolderAsync(AccountHolderr accountHolder)
        {
            if (accountHolder == null)
            {
                throw new ArgumentNullException(nameof(accountHolder));
            }
            await _context.AccountHolders.AddAsync(accountHolder);
            await _context.SaveChangesAsync();
            return accountHolder;
        }


        public Task<AccountHolderr> GetAccountHolderByIdAsync(int id)
        {
            var accountHolder = _context.AccountHolders.Where(x => x.Id == id ).FirstOrDefaultAsync();
            if (accountHolder == null)
            {
                throw new KeyNotFoundException($"Account holder with ID {id} not found");
            }
            return accountHolder!;
        }

        public async Task<IEnumerable<AccountHolderr>> GetAllAccountHoldersAsync()
        {
            return await _context.AccountHolders.ToListAsync();
        }

        public async Task<AccountHolderr> UpdateAccountHolderAsync(int id ,AccountHolderr accountHolder)
        {
            if (accountHolder == null)
            {
                throw new ArgumentNullException(nameof(accountHolder));
            }
            var existingAccountHolder = _context.AccountHolders.Where(x => x.Id == id).FirstOrDefault();
            if (existingAccountHolder == null)
            {
                throw new KeyNotFoundException($"Account holder with ID {accountHolder.Id} not found");
            }

            // Detach the existing entity to avoid tracking conflicts
            _context.Entry(existingAccountHolder).State = EntityState.Detached;


            _context.AccountHolders.Update(accountHolder);
            await _context.SaveChangesAsync();

            return accountHolder;
        }
    }
}
