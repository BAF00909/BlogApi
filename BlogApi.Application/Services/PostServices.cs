using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;

namespace BlogApi.Services
{
    public class PostServices : IPostService
    {
        private readonly ILogger<PostServices> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public PostServices(IUnitOfWork unitOfWork ,ILogger<PostServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Post> CreateAsync(PostDto postDto)
        {
            _logger.LogInformation("Creating new post: {Title}", postDto.Title);
            var post = new Post();
            post.Update(postDto.Title, postDto.Content);
            post.CreateAt = DateTime.UtcNow;
            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return post;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Delete post by ID: {Id}", id);
            Post? post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return false;
            await _unitOfWork.Posts.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<(List<Post>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order)
        {
            _logger.LogInformation($"Fetching post, page {page}, pageSize {pageSize}");
            return await _unitOfWork.Posts.GetAllAsync(page, pageSize, title, sortBy, order);
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Posts.GetByIdAsync(id);
        }
        public async Task<bool> UpdateAsync(int id, PostDto postDto)
        {
            _logger.LogInformation("Update post by ID: {Id}", id);
            Post? currentPost = await _unitOfWork.Posts.GetByIdAsync(id);
            if (currentPost == null) return false;
            currentPost.Update(postDto.Title, postDto.Content);
            await _unitOfWork.Posts.UpdateAsync(currentPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
