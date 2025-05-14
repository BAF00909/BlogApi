using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync(int page, int pageSize);
        Task<Post?> GetByIdAsync(int id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
