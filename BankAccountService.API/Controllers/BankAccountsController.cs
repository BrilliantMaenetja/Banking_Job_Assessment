using BankAccountService.Application.Services;
using BankAccountService.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankAccountsController(IBankService service)
        {
            _bankService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _bankService.GetAllAccountsAsync();
            return Ok(accounts);
        }
        [HttpGet]
        [Route("{accountId:int}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            var account = await _bankService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("AddAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] BankAcount account)
        {
            if (account == null)
            {
                return BadRequest("Account cannot be null");
            }
            var createdAccount = await _bankService.CreateAccountAsync(account);
  
            return CreatedAtAction(nameof(GetAccountById), new { accountId = createdAccount.Id }, createdAccount);
        }

        [HttpPost]
        [Route("{accountId:int}/deposit")]
        public async Task<IActionResult> Deposit(int accountId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Deposit amount must be greater than zero");
            }
            var result = await _bankService.DepositAsync(accountId, amount);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        [Route("{accountId:int}/withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than zero");
            }
            var result = await _bankService.WithdrawAsync(accountId, amount);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
