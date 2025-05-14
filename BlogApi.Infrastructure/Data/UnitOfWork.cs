using BlogApi.Application.Interfaces;

namespace BlogApi.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;
        public IPostRepository Posts { get; }
        public UnitOfWork(BlogContext context, IPostRepository postRepository)
        {
            _context = context;
            Posts = postRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
