using BlogApi.Application.Interfaces;
using BlogApi.Application.Services;
using BlogApi.Infrastructure.Data;
using BlogApi.Infrastructure.Repositories;
using BlogApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;

namespace BlogApi.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string CORS_POLICY = "AllowFrontend";
            var builder = WebApplication.CreateBuilder(args);
            // Add serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "logs/blogapi-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
            builder.Host.UseSerilog();
            // Add authorization
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            // Add services to container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add CORS
            string[] allowPath = new string[] { builder.Configuration["AllowClientOriginal"] };
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY, builder =>
                {
                    builder.WithOrigins(allowPath)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            // Add connection to db
            string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("BlogApi.Infrastructure")).EnableSensitiveDataLogging());
            // Register dependensies
            builder.Services.AddScoped<IPostRepository, PostRepositories>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddLogging(builder => builder.AddConsole());
            // Add swagger
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(CORS_POLICY);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
