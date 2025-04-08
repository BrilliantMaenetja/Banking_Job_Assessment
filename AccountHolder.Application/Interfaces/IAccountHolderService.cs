using AccountHolder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountHolder.Application.Interfaces
{
    public interface IAccountHolderService
    {
        Task<IEnumerable<AccountHolderr>> GetAllAccountHoldersAsync();
        Task<AccountHolderr> GetAccountHolderByIdAsync(int id);
        Task<AccountHolderr> CreateAccountHolderAsync(AccountHolderr accountHolder);
        Task<AccountHolderr> UpdateAccountHolderAsync(int id, AccountHolderr accountHolder);

    }
}
