using BlogApi.Application.Dtos;
using BlogApi.Domain.Entities;

namespace BlogApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<(List<PostDto>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order);
        Task<Post?> GetByIdAsync(int id);
        Task<PostDto> CreateAsync(PostDto postDto);
        Task<bool> UpdateAsync(int id, PostDto postDto);
        Task<bool> DeleteAsync(int id);
        Task<string> UploadImagesAsync(int id, IFormFile image);
    }
}
