using BlogApi.Models;

namespace BlogApi.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(PostDto postDto);
        Task UpdateAsync(int id, PostDto postDto);
        Task DeleteAsync(int id);
    }
}
