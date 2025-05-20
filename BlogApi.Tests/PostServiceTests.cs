using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BlogApi.Tests
{
    public class PostServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly ILogger<PostService> _mockLogger;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPostRepository = new Mock<IPostRepository>();
            _mockUnitOfWork.Setup(u => u.Posts).Returns(_mockPostRepository.Object);
            _mockLogger = Mock.Of<ILogger<PostService>>();
            _postService = new PostService(_mockUnitOfWork.Object, _mockLogger);
        }

        [Fact]
        public async Task CreateAsync_ValidPostDto_ReturnsCreatedPost()
        {
            // Arrange
            var postDto = new PostDto { Title = "Test Post", Content = "Content" };
            var post = new Post { Id = 1, Title = "Test Post", Content = "Content", CreateAt = DateTime.UtcNow };
            _mockPostRepository.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Callback<Post>(p => p.Id = 1); // Ёмулируем присвоение Id

            // Act
            var result = await _postService.CreateAsync(postDto);

            // Assert
            result.ShouldNotBeNull();
            result.Title.ShouldBe(postDto.Title);
            result.Content.ShouldBe(postDto.Content);
            _mockPostRepository.Verify(r => r.AddAsync(It.Is<Post>(p => p.Title == postDto.Title)), Times.Once());
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_WithTitleFilter_ReturnsFilteredPosts()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Test Post", Content = "Content", CreateAt = DateTime.UtcNow },
                new Post { Id = 2, Title = "Other Post", Content = "Content", CreateAt = DateTime.UtcNow }
            };
            _mockPostRepository.Setup(r => r.GetAllAsync(1, 10, "Test", null, null))
                .ReturnsAsync((new List<Post> { posts[0] }, 1));

            // Act
            var (result, totalCount) = await _postService.GetAllAsync(1, 10, "Test", null, null);

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].Title.ShouldBe("Test Post");
            totalCount.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateAsync_ValidIdAndPostDto_ReturnsTrue()
        {
            // Arrange
            var postDto = new PostDto { Title = "Updated Post", Content = "Updated Content" };
            var existingPost = new Post { Id = 1, Title = "Test Post", Content = "Content", CreateAt = DateTime.UtcNow };
            _mockPostRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingPost);
            _mockPostRepository.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
                .Callback<Post>(p => existingPost.Title = p.Title);

            // Act
            var result = await _postService.UpdateAsync(1, postDto);

            // Assert
            result.ShouldBeTrue();
            existingPost.Title.ShouldBe(postDto.Title);
            _mockPostRepository.Verify(r => r.UpdateAsync(It.IsAny<Post>()), Times.Once());
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var postDto = new PostDto { Title = "Updated Post", Content = "Updated Content" };
            _mockPostRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Post?)null);

            // Act
            var result = await _postService.UpdateAsync(999, postDto);

            // Assert
            result.ShouldBeFalse();
            _mockPostRepository.Verify(r => r.UpdateAsync(It.IsAny<Post>()), Times.Never());
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never());
        }
    }
}