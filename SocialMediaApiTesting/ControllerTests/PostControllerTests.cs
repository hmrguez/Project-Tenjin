using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SocialMediaRepositories.Controllers;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SocialMediaApiTesting.ControllerTests
{
    public class PostControllerTests
    {
        private readonly PostController _controller;
        private readonly Mock<IPostRepository> _mockRepository;

        public PostControllerTests()
        {
            _mockRepository = new Mock<IPostRepository>();
            _controller = new PostController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetPosts_ReturnsOkObjectResult_WithListOfPosts()
        {
            // Arrange
            var posts = new List<Post>
            {
                new() { Id = Guid.NewGuid(), UserAlias = "user1", Text = "Post 1", Picture = "picture1.jpg", LikeCount = 5 },
                new() { Id = Guid.NewGuid(), UserAlias = "user2", Text = "Post 2", Picture = "picture2.jpg", LikeCount = 10 },
            };
            _mockRepository.Setup(x => x.PostsAsync()).ReturnsAsync(posts);

            // Act
            var result = await _controller.GetPosts();

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(posts);
        }

        [Fact]
        public async Task GetPost_ReturnsOkObjectResult_WithPost()
        {
            // Arrange
            var id = Guid.NewGuid();
            var post = new Post { Id = id, UserAlias = "user1", Text = "Post 1", Picture = "picture1.jpg", LikeCount = 5 };
            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(post);

            // Act
            var result = await _controller.GetPost(id);

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(post);
        }

        [Fact]
        public async Task CreatePost_ReturnsOkObjectResult_WithListOfPosts()
        {
            // Arrange
            var post = new Post { Id = Guid.NewGuid(), UserAlias = "user1", Text = "Post 1", Picture = "picture1.jpg", LikeCount = 5 };
            _mockRepository.Setup(x => x.InsertOneAsync(post));

            // Act
            var result = await _controller.CreatePost(post);

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.PostsAsync());
        }

        [Fact]
        public async Task UpdatePost_ReturnsOkObjectResult_WithListOfPosts()
        {
            // Arrange
            var id = Guid.NewGuid();
            var post = new Post { Id = id, UserAlias = "user1", Text = "Post 1", Picture = "picture1.jpg", LikeCount = 5 };
            var updatedPost = new Post { Id = id, UserAlias = "user2", Text = "Post 2", Picture = "picture2.jpg", LikeCount = 10 };
            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(post);

            // Act
            var result = await _controller.UpdatePost(updatedPost);

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.PostsAsync());
        }

        [Fact]
        public async Task DeletePost_ReturnsOkObjectResult_WithListOfPosts()
        {
            // Arrange
            var id = Guid.NewGuid();
            var post = new Post { Id = id, UserAlias = "user1", Text = "Post 1", Picture = "picture1.jpg", LikeCount = 5 };
            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(post);

            // Act
            var result = await _controller.DeletePost(id);

            // Assert
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.PostsAsync());
        }
    }
}