using BankAccountService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountService.Application.Services
{
    public interface IBankService
    {
        Task<BankAcount> CreateAccountAsync(BankAcount accountHolderName);
        Task<BankAcount> GetAccountByIdAsync(int accountId);
        Task<List<BankAcount>> GetAllAccountsAsync();
        Task<bool> DepositAsync(int accountId, decimal amount);
        Task<bool> WithdrawAsync(int accountId, decimal amount);
    }
}
