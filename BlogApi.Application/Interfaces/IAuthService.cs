using BlogApi.Application.Dtos;

namespace BlogApi.Application.Interfaces
{
    public interface IAuthService
    {
        string GetJwtToken(LoginDto loginDtao, IConfiguration configurations);
    }
}
