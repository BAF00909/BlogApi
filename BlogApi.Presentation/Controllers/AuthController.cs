using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configurations;
        public AuthController(IAuthService authService, IConfiguration configurations) { 
            _authService = authService;
            _configurations = configurations;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginTdo) {
            if (loginTdo.UserName == "admin" && loginTdo.Password == "password")
            {
                var token = _authService.GetJwtToken(loginTdo, _configurations);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
