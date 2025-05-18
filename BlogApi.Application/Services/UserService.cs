using Azure.Identity;
using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;

namespace BlogApi.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public UserService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.UserName) || string.IsNullOrWhiteSpace(registerDto.Password))
            {
                throw new ArgumentException("Username and Password are required");
            }
            var existedUser = await _unitOfWork.Users.GetByUserNameAsync(registerDto.UserName);
            if (existedUser != null)
            {
                throw new InvalidOperationException("UserName already exists");
            }
            var newUser = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow,
            };
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<string> LogInAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetByUserNameAsync(loginDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
            return _authService.GetJwtToken(loginDto);
        }
    }
}
