using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Title is required")]
        public string Content { get; set; } = null!;
    }
}
