using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountService.Domain.Models
{
    public class BankAcount
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public AccountStatus Status { get; set; }


#pragma warning disable
        public BankAcount()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }


#pragma warning restore
        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void WithdrawalUpdate(decimal amount , AccountTypeEnum accountType , AccountStatus status )
        {

            if (status == AccountStatus.Inactive)
            {
                throw new InvalidOperationException("Account is inactive");
            }
            else
            {
                if (accountType == AccountTypeEnum.FixedDeposit)
                {
                    Balance = 0;
                }
                else
                {
                    if (amount > Balance)
                    {
                        throw new InvalidOperationException("Insufficient funds");
                    }
                    else if (amount < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
                    }
                    else
                        Balance -= amount;
                    UpdatedAt = DateTime.UtcNow;
                }
            }

          
        }

        public enum AccountTypeEnum
        {
            Savings,
            Cheque,
            FixedDeposit
        }
        public enum AccountStatus
        {
            Active,
            Inactive
        }
    }
}
