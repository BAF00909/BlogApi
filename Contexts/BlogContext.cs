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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "The first post in the blog", Content = "This is my first post in the blog", CreateAt = DateTime.Now },
                new Post { Id = 2, Title = "The second post in the blog", Content = "This is my second post in the blog", CreateAt = DateTime.Now },
                new Post { Id = 3, Title = "The third post in the blog", Content = "This is my third post in the blog", CreateAt = DateTime.Now }
                );
        }
    }
}
