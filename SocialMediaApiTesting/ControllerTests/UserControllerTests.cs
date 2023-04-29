using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SocialMediaRepositories.Controllers;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaApiTesting.ControllerTests;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly Mock<IUserRepository> _mockRepository;

    public UserControllerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _controller = new UserController(_mockRepository.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkObjectResult_WithListOfUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new()
            {
                Alias = "user1", Name = "User 1", Description = "User 1 description", ProfilePicture = "profile1.jpg",
                EmailAddress = "user1@example.com"
            },
            new()
            {
                Alias = "user2", Name = "User 2", Description = "User 2 description", ProfilePicture = "profile2.jpg",
                EmailAddress = "user2@example.com"
            }
        };
        _mockRepository.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);

        var userResponse = users.Select(u => new UserResponse
        (
            u.Alias,
            u.Name,
            u.EmailAddress,
            u.ProfilePicture,
            u.Description
        )).ToList();


        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();
        okObjectResult?.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task GetUser_ReturnsOkObjectResult_WithUser()
    {
        // Arrange
        var alias = "user1";
        var user = new User
        {
            Alias = alias, Name = "User 1", Description = "User 1 description", ProfilePicture = "profile1.jpg",
            EmailAddress = "user1@example.com"
        };
        _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(user);
        
        var userResponse = new UserResponse(
            user.Alias,
            user.Name,
            user.EmailAddress,
            user.ProfilePicture,
            user.Description
        );
        
        // Act
        var result = await _controller.GetUser(alias);

        // Assert
        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();
        okObjectResult?.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOkObjectResult()
    {
        // Arrange
        var alias = "user1";
        var user = new UserResponse(alias, "User 1 updated", "temp@gmail.com", "temppicture", "tempdescription");
        var dbUser = new User
        {
            Alias = alias, Name = "User 1", Description = "User 1 description", FollowerCount = 10,
            ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com"
        };
        _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(dbUser);

        // Act
        var result = await _controller.UpdateUser(user);

        // Assert
        var okObjectResult = result.Result as OkResult;
        okObjectResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkObjectResult()
    {
        // Arrange
        var alias = "user1";
        var user = new User
        {
            Alias = alias, Name = "User 1", Description = "User 1 description", FollowerCount = 10,
            ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com"
        };
        _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(user);
        _mockRepository.Setup(x => x.DeleteAsync(user));

        // Act
        var result = await _controller.DeleteUser(alias);

        // Assert
        var okObjectResult = result.Result as OkResult;
        okObjectResult.Should().NotBeNull();
    }
}