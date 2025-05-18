using BlogApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Data
{
    public class BlogContext: DbContext
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public BlogContext(DbContextOptions<BlogContext> options): base(options)
        {
            // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(p => p.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
