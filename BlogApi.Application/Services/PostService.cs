using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;

namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork ,ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<PostDto> CreateAsync(PostDto postDto)
        {
            if (!string.IsNullOrWhiteSpace(postDto.Title) || !string.IsNullOrWhiteSpace(postDto.Content))
                throw new ArgumentException("Title and content are required");

            _logger.LogInformation("Creating new post: {Title}", postDto.Title);

            var post = new Post();
            post.Update(postDto.Title, postDto.Content);
            post.CreateAt = DateTime.UtcNow;

            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateAt = post.CreateAt,
                ImagePath = post.ImagePath,
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Delete post by ID: {Id}", id);
            Post? post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return false;
            await _unitOfWork.Posts.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<(List<PostDto>, int TotalCount)> GetAllAsync(int page, int pageSize, string? title, string? sortBy, string? order)
        {
            _logger.LogInformation($"Fetching post, page {page}, pageSize {pageSize}");
            var (posts, totalCount) = await _unitOfWork.Posts.GetAllAsync(page, pageSize, title, sortBy, order);
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreateAt = p.CreateAt,
                ImagePath = p.ImagePath,
            }).ToList();
            return (postDtos, totalCount);
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Posts.GetByIdAsync(id);
        }
        public async Task<bool> UpdateAsync(int id, PostDto postDto)
        {
            _logger.LogInformation("Update post by ID: {Id}", id);
            Post? currentPost = await _unitOfWork.Posts.GetByIdAsync(id);
            if (currentPost == null) return false;
            currentPost.Update(postDto.Title, postDto.Content);
            await _unitOfWork.Posts.UpdateAsync(currentPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<string> UploadImagesAsync(int id, IFormFile image)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null)
                throw new KeyNotFoundException("Post not found");
            if (image == null || image.Length == 0)
                throw new ArgumentException("Image is required");
            var allowExtensions = new[] { ".jpeg", ".jpg", ".png"};
            var extension = Path.GetExtension(image.FileName).ToLower();
            if (!allowExtensions.Contains(extension))
                throw new ArgumentException("Invalid image format. Only .jpg .jpeg, .png are allowed");
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine("uploads/images", fileName);
            Directory.CreateDirectory(Path.Combine("uploads", "images"));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            post.ImagePath = $"/images/{fileName}";
            await _unitOfWork.Posts.UpdateAsync(post);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Uploaded image for post {PostId}: {ImagePath}", post.Id, post.ImagePath);
            return post.ImagePath;
        }
    }
}
