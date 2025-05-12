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
        public PostServices(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Post> CreateAsync(PostDto postDto)
        {
            var post = new Post() { Title = postDto.Title, Content = postDto.Content};
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
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.ToListAsync();
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
        }
    }
}
