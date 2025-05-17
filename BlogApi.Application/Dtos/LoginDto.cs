using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class LoginDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "UserName cannot less 3 characters")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
