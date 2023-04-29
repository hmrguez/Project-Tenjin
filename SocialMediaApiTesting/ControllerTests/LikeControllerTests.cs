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
    public class LikeControllerTests
    {
        private readonly LikeController _controller;
        private readonly Mock<ILikeRepository> _mockRepository;
        private readonly Mock<IPostRepository> _mockPostRepository;

        public LikeControllerTests()
        {
            _mockRepository = new Mock<ILikeRepository>();
            _mockPostRepository = new Mock<IPostRepository>();
            _controller = new LikeController(_mockRepository.Object, _mockPostRepository.Object);
        }

        [Fact]
        public async Task GetLikes_ReturnsOkObjectResult_WithListOfLikes()
        {
            // Arrange
            var likes = new List<Like>
            {
                new() { Id = Guid.NewGuid(), PostId = Guid.NewGuid() },
                new() { Id = Guid.NewGuid(), PostId = Guid.NewGuid() },
            };
            _mockRepository.Setup(x => x.LikesAsync()).ReturnsAsync(likes);

            // Act
            var result = await _controller.GetLikes();

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(likes);
        }

        [Fact]
        public async Task GetLike_WithValidId_ReturnsOkObjectResult_WithLike()
        {
            // Arrange
            var likeId = Guid.NewGuid();
            var like = new Like { Id = likeId, PostId = Guid.NewGuid() };
            _mockRepository.Setup(x => x.GetByIdAsync(likeId)).ReturnsAsync(like);

            // Act
            var result = await _controller.GetLike(likeId);

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(like);
        }

        [Fact]
        public async Task GetLike_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var likeId = Guid.NewGuid();
            _mockRepository.Setup(x => x.GetByIdAsync(likeId)).ReturnsAsync(null as Like);

            // Act
            var result = await _controller.GetLike(likeId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateLike_IncrementsPostLikeCount_AndInsertsNewLike()
        {
            // Arrange
            var postId = new Guid();
            var userAlias = "testuser";
            var likeRequest = new LikeRequest(userAlias, postId);
            var post = new Post { Id = postId, LikeCount = 0 };
            _mockPostRepository.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(post);

            // Act
            var result = await _controller.CreateLike(likeRequest);

            // Assert
            _mockPostRepository.Verify(x => x.UpdateOneAsync(post), Times.Once);
            _mockRepository.Verify(x => x.InsertOneAsync(It.Is<Like>(l => l.PostId == postId && l.UserAlias == userAlias)), Times.Once);
            result.Result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteLike_WithValidId_ReturnsOkObjectResult_WithListOfLikes()
        {
            // Arrange
            var likeId = Guid.NewGuid();
            var like = new Like { Id = likeId, PostId = Guid.NewGuid() };
            _mockRepository.Setup(x => x.GetByIdAsync(likeId)).ReturnsAsync(like);
            _mockRepository.Setup(x => x.DeleteAsync(like)).Returns(Task.CompletedTask);
            _mockRepository.Setup(x => x.LikesAsync()).ReturnsAsync(new List<Like>());

            // Act
            var result = await _controller.DeleteLike(likeId);

            // Assert
            var okObjectResult = result as OkResult;
            okObjectResult.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteLike_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var likeId = Guid.NewGuid();
            _mockRepository.Setup(x => x.GetByIdAsync(likeId)).ReturnsAsync(null as Like);

            // Act
            var result = await _controller.DeleteLike(likeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}