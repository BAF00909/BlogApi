using BlogApi.Contexts;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
