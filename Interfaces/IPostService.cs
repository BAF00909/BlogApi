using BlogApi.Application.Dtos;
using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllAsync(int page, int pageSize);
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(PostDto postDto);
        Task<bool> UpdateAsync(int id, PostDto postDto);
        Task<bool> DeleteAsync(int id);
    }
}
