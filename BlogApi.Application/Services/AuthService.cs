using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configurations;
        public AuthService(IConfiguration configurations)
        {
            _configurations = configurations;
        }
        public string GetJwtToken(LoginDto loginDtao)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginDtao.UserName) };
            var jwt = new JwtSecurityToken(
                issuer: _configurations["Jwt:Issuer"],
                audience: _configurations["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
