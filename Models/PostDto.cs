using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class PostDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
    }
}
