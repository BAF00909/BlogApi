using BlogApi.Contexts;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogApi.Services
{
    public class PostServices : IPostService
    {
        private readonly BlogContext _dbContext;
        private readonly ILogger<PostServices> _logger;
        public PostServices(BlogContext dbContext, ILogger<PostServices> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Post> CreateAsync(PostDto postDto)
        {
            var post = new Post() { Title = postDto.Title, Content = postDto.Content};
            _logger.LogInformation("Создание поста: {Title}", post.Title);
            await _dbContext.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }
        public async Task DeleteAsync(int id)
        {
            Post? post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) throw new KeyNotFoundException("Пост не найден");
            _dbContext.Remove(post);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Удаление поста: {Id}", post.Id);
        }
        public async Task<List<Post>> GetAllAsync(int page, int pageSize)
        {
            return await _dbContext.Posts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task UpdateAsync(int id, PostDto postDto)
        {
            Post? currentPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (currentPost == null) throw new KeyNotFoundException("Пост не найден");
            currentPost.Title = postDto.Title;
            currentPost.Content = postDto.Content;
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Изменение поста: {Id}", currentPost.Id);
        }
    }
}
