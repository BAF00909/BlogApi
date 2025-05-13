using BlogApi.Contexts;
using BlogApi.Interfaces;
using BlogApi.Models;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IPostService, PostServices>();
            builder.Services.AddControllers();
            builder.Services.AddLogging(builder => builder.AddConsole());
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
