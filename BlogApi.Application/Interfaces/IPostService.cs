using BlogApi.Application.Dtos;
using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<(List<Post>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order);
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(PostDto postDto);
        Task<bool> UpdateAsync(int id, PostDto postDto);
        Task<bool> DeleteAsync(int id);
    }
}
