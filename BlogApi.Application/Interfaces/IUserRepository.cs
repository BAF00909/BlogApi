using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string username);
        Task AddAsync(User user);
    }
}
