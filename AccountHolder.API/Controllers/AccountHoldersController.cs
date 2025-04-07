using AccountHolder.Application.Interfaces;
using AccountHolder.Domain.Models;
using Asp.Versioning;
using Messaging.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountHolder.API.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountHoldersController : ControllerBase
    {
        private readonly IAccountHolderService _holderService;
        private readonly ILogger<AccountHoldersController> _logger;
        private readonly IMessageProducer _messageProducer;
        protected string _queueName = "accountHolderQueue";

        public AccountHoldersController(IAccountHolderService service , ILogger<AccountHoldersController> logger,
            IMessageProducer producer )
        {

            _holderService = service;
            _messageProducer = producer;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAccountHolders()
        {
            var accountHolders = await _holderService.GetAllAccountHoldersAsync();
            return Ok(accountHolders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountHolder([FromBody] AccountHolderr accountHolder)
        {
            if (accountHolder == null)
            {
                return BadRequest("Account holder cannot be null");
            }

            var createdAccountHolder = await _holderService.CreateAccountHolderAsync(accountHolder);

            //Send Message upon creation
            _messageProducer.SendMessage(accountHolder, _queueName);

            return Ok(createdAccountHolder);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAccountHolderById(int id)
        {
            var accountHolder = await _holderService.GetAccountHolderByIdAsync(id);
            if (accountHolder == null)
            {
                return NotFound($"Account holder with ID {id} not found");
            }
            return Ok(accountHolder);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateAccountHolder(int id, [FromBody] AccountHolderr accountHolder)
        {
            if (accountHolder == null || accountHolder.Id != id)
            {
                return BadRequest("Account holder data is invalid");
            }
            var updatedAccountHolder = await _holderService.UpdateAccountHolderAsync(accountHolder);
            _messageProducer.SendMessage(accountHolder, _queueName);
            if (updatedAccountHolder == null)
            {
                return NotFound($"Account holder with ID {id} not found");
            }
            return Ok(updatedAccountHolder);
        }

    }
}
