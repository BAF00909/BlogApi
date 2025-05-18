using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogApi.Infrastructure.Data
{
    public class BlogContextFactory: IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=blogdb;Trusted_Connection=True;");
            return new BlogContext(optionsBuilder.Options);
        }
    }
}
