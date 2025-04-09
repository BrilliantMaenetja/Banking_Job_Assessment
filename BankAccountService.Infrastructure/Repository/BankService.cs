using BankAccountService.Application.Services;
using BankAccountService.Domain.Models;
using BankAccountService.Infrastructure.Data;
using Messaging.Shared.Producers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace BankAccountService.Infrastructure.Repository
{
    public class BankService : IBankService
    {
        private readonly BankAccountDbContext _context;
        private readonly MessageProducer _messageProducer;

        public BankService(BankAccountDbContext context , MessageProducer producer )
        {
            _context = context;
            _messageProducer = producer;
        }


        public Task<BankAcount> CreateAccountAsync(BankAcount accountHolderName)
        {
            if (accountHolderName == null)
            {
                throw new ArgumentNullException(nameof(accountHolderName));
            }
            
            var newAccount = new BankAcount
            {
                Id = accountHolderName.Id,
                AccountHolderName = accountHolderName.AccountHolderName,
                AccountNumber = accountHolderName.AccountNumber,
                Balance = accountHolderName.Balance,
                AccountType = accountHolderName.AccountType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.AddAsync(newAccount);
            _context.SaveChangesAsync();
            return Task.FromResult(newAccount);
        }

        public Task<bool> DepositAsync(int accountId, decimal amount)
        {
            var account = _context.BankAcounts.Where(id => id.AccountNumber.Equals(accountId)).FirstOrDefault();

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            else
            {
                account.UpdateBalance(amount);
                _context.BankAcounts.Update(account);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<BankAcount> GetAccountByIdAsync(int accountId)
        {
            if(accountId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(accountId));
            }
            else
            {
                var account = _context.BankAcounts.Where(id => id.Equals(accountId)).FirstOrDefault();
                if (account == null)
                {
                    throw new ArgumentNullException(nameof(account));
                }
                return Task.FromResult(account);
            }
        }

        public Task<List<BankAcount>> GetAllAccountsAsync()
        {
            var accounts =  _context.BankAcounts.ToListAsync();
            return accounts;
        }

        public Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            var account = _context.BankAcounts.Where(acc => acc.AccountNumber == accountId.ToString()).FirstOrDefault();
       
             account!.WithdrawalUpdate(amount, account.AccountType , account.Status);
             var outputt = _context.BankAcounts.Update(account);
             _context?.SaveChangesAsync(); 

            return Task.FromResult(true);
            
        }
    }
}
