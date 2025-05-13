using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Contexts
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
