using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;
using BlogApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;
        public UserRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
