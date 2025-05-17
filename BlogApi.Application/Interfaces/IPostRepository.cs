using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IPostRepository
    {
        Task<(List<Post>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order);
        Task<Post?> GetByIdAsync(int id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
