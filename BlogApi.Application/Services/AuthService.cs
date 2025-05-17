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
        public string GetJwtToken(LoginDto loginDtao, IConfiguration configurations)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginDtao.UserName) };
            var jwt = new JwtSecurityToken(
                issuer: configurations["Jwt:Issuer"],
                audience: configurations["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
