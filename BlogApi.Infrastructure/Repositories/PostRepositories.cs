using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;
using BlogApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories
{
    public class PostRepositories : IPostRepository
    {
        private readonly BlogContext _context;
        public PostRepositories(BlogContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<(List<Post>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order)
        {
            var query = _context.Posts.AsQueryable();
            if(!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(p => p.Title.ToLower().Contains(title.ToLower()));
            }
            int totalCount = await query.CountAsync();
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                switch(sortBy.ToLower())
                {
                    case "title": 
                        query = order?.ToLower() == "desc" ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title);
                        break;
                    case "createat":
                        query = order?.ToLower() == "desc" ? query.OrderByDescending(p => p.CreateAt) : query.OrderBy(p => p.CreateAt);
                        break;
                    default: 
                        query = query.OrderBy(p => p.Id);
                        break;
                } 
            } else
            {
                query = query.OrderBy(p => p.Id);
            }

            var posts = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return (posts, totalCount);
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }
        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
