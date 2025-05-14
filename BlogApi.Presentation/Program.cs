using BlogApi.Application.Interfaces;
using BlogApi.Infrastructure.Data;
using BlogApi.Infrastructure.Repositories;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add connection to db
            string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
            // Register dependensies
            builder.Services.AddScoped<IPostRepository, PostRepositories>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPostService, PostServices>();
            builder.Services.AddLogging(builder => builder.AddConsole());

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
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
