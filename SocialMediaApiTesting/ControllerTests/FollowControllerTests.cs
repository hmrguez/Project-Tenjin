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
    public class FollowControllerTests
    {
        private readonly FollowController _controller;
        private readonly Mock<IFollowRepository> _mockRepository;

        public FollowControllerTests()
        {
            _mockRepository = new Mock<IFollowRepository>();
            _controller = new FollowController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetFollows_ReturnsOkObjectResult_WithListOfFollows()
        {
            // Arrange
            var follows = new List<Follow>
            {
                new() { Id = Guid.NewGuid(), DateStarted = DateTime.UtcNow, FollowerAlias = "user1", FollowedAlias = "user2" },
                new() { Id = Guid.NewGuid(), DateStarted = DateTime.UtcNow, FollowerAlias = "user1", FollowedAlias = "user3" }
            };
            _mockRepository.Setup(repo => repo.FollowsAsync()).ReturnsAsync(follows);

            // Act
            var result = await _controller.GetFollows();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedFollows = okResult.Value.Should().BeAssignableTo<IEnumerable<Follow>>().Subject;
            returnedFollows.Should().HaveCount(2).And.BeEquivalentTo(follows);
        }

        [Fact]
        public async Task GetFollowById_WithValidId_ReturnsOkObjectResult_WithFollow()
        {
            // Arrange
            var followId = Guid.NewGuid();
            var follow = new Follow { Id = followId, DateStarted = DateTime.UtcNow, FollowerAlias = "user1", FollowedAlias = "user2" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(followId)).ReturnsAsync(follow);
        
            // Act
            var result = await _controller.GetFollow(followId);
        
            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedFollow = okResult.Value.Should().BeAssignableTo<Follow>().Subject;
            returnedFollow.Should().BeEquivalentTo(follow);
        }
        //
        [Fact]
        public async Task GetFollowById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var followId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetByIdAsync(followId)).ReturnsAsync(null as Follow);
        
            // Act
            var result = await _controller.GetFollow(followId);
        
            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        //
        [Fact]
        public async Task AddFollow_WithValidFollow_ReturnsCreatedAtActionResult_WithCreatedFollow()
        {
            // Arrange
            var follow = new Follow { FollowerAlias = "user1", FollowedAlias = "user2" };
            _mockRepository.Setup(repo => repo.InsertOneAsync(follow));
            _mockRepository.Setup(repo => repo.FollowsAsync()).ReturnsAsync(new List<Follow> { follow });
            
            // Act
            var result = await _controller.CreateFollow(follow);
        
            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            createdAtActionResult.Value.Should().BeOfType<List<Follow>>().And
                .BeEquivalentTo(new List<Follow> { follow });
        }
        //
        [Fact]
        public async Task DeleteFollow_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var followId = Guid.NewGuid();
            var follow = new Follow() { Id = followId };
            _mockRepository.Setup(repo => repo.GetByIdAsync(followId)).ReturnsAsync(follow);
            _mockRepository.Setup(repo => repo.DeleteAsync(follow)).Verifiable();
        
            // Act
            var result = await _controller.DeleteFollow(followId);
        
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockRepository.Verify();
        }
        //
        [Fact]
        public async Task DeleteFollow_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var followId = Guid.NewGuid();
            var follow = new Follow() { Id = followId };
            _mockRepository.Setup(repo => repo.GetByIdAsync(followId)).ReturnsAsync(null as Follow);
            // _mockRepository.Setup(repo => repo.DeleteAsync(follow)).ThrowsAsync(new KeyNotFoundException());
            
            // Act
            var result = await _controller.DeleteFollow(followId);
        
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}