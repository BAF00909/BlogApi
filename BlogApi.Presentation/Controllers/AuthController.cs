using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService) { 
            _userService = userService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginTdo) {
            try
            {
                var token = _userService.LogInAsync(loginTdo);
                return Ok(new { token });
            } catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                await _userService.RegisterAsync(registerDto);
                return NoContent();
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
