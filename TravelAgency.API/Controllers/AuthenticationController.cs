using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.DTOs;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { Token = token });
        }
    }
}