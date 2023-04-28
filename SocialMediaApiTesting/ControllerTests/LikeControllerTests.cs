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

        public LikeControllerTests()
        {
            _mockRepository = new Mock<ILikeRepository>();
            // _controller = new LikeController(_mockRepository.Object);
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

        // [Fact]
        // public async Task CreateLike_ReturnsOkObjectResult_WithListOfLikes()
        // {
        //     // Arrange
        //     var like = new Like { Id = Guid.NewGuid(), PostId = Guid.NewGuid() };
        //     _mockRepository.Setup(x => x.InsertOneAsync(like)).Returns(Task.CompletedTask);
        //     _mockRepository.Setup(x => x.LikesAsync()).ReturnsAsync(new List<Like> { like });
        //
        //     // Act
        //     var result = await _controller.CreateLike(like);
        //
        //     // Assert
        //     var okObjectResult = result.Result as OkObjectResult;
        //     okObjectResult.Should().NotBeNull();
        //     okObjectResult?.Value.Should().BeEquivalentTo(new List<Like> { like });
        // }

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
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult?.Value.Should().BeEquivalentTo(new List<Like>());
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