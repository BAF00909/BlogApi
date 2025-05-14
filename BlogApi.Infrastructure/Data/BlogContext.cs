using BlogApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Data
{
    public class BlogContext: DbContext
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public BlogContext(DbContextOptions<BlogContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
