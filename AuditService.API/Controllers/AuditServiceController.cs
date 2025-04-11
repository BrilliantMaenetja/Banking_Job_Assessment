using AuditService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditServiceController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditServiceController(IAuditService audit)
        {
            _auditService = audit;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAll(int id)
        {
            var audits = await _auditService.GetActions(id);
            return Ok(audits);
        }
        [HttpPost]
        public async Task<IActionResult> LogAction([FromBody] int id, string action)
        {
            if (id <= 0 || string.IsNullOrEmpty(action))
            {
                return BadRequest("Invalid input");
            }
            var result = await _auditService.LogAction(id, action);
            if (result)
            {
                return Ok("Action logged successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to log action");
            }
        }

    }
}
