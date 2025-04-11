using JwtAuthenticationManager.Common.Models;
using JwtAuthenticationManager.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;


        public AuthController()
        {
            _jwtTokenService = new JwtTokenService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var token = _jwtTokenService.GenerateAuthToken(loginModel);
            if (token == null)
                return Unauthorized(new { Message = "Invalid username or password." });

            return Ok(token);
        }
    }
}