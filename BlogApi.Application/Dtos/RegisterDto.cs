using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
