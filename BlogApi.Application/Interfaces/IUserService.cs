using BlogApi.Application.Dtos;

namespace BlogApi.Application.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<string> LogInAsync(LoginDto loginDto);
    }
}
