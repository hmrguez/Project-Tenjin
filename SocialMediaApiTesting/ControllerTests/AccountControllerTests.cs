using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SocialMediaRepositories.Controllers;
using SocialMediaRepositories.Dtos;
using SocialMediaRepositories.Helper;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;
using Xunit.Abstractions;

namespace SocialMediaApiTesting.ControllerTests;

public class AccountControllerTests
{
    private readonly AccountController _controller;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IUserRepository> _repositoryMock;

    public AccountControllerTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _repositoryMock = new Mock<IUserRepository>();
        _controller = new AccountController(_configurationMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void Login_ReturnsNotFoundObjectResult_WhenInvalidUserDtoIsPassed()
    {
        // Arrange
        var userDto = new UserDto { Username = "invaliduser", Password = "invalidpassword" };
        _repositoryMock.Setup(x => x.GetByAlias(userDto.Username)).Returns((User)null);

        // Act
        var result = _controller.Login(userDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var message = Assert.IsType<string>(notFoundResult.Value);
        Assert.Equal("Wrong credentials", message);
    }

    [Fact]
    public async Task Register_ReturnsOkObjectResult_WhenValidCreateUserDtoIsPassed()
    {
        // Arrange
        var userDto = new CreateUserDto { Name = "Test User", Alias = "testuser", Email = "testuser@example.com", Password = "testpassword" };
        var user = new User { Name = "Test User", Alias = "testuser", EmailAddress = "testuser@example.com" };
        _repositoryMock.Setup(x => x.InsertOneAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Register(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Registered User", message);
    }

    [Fact]
    public void Authenticate_ReturnsNull_WhenInvalidUserDtoIsPassed()
    {
        // Arrange
        var userDto = new UserDto { Username = "invaliduser", Password = "invalidpassword" };
        _repositoryMock.Setup(x => x.GetByAlias(userDto.Username)).Returns((User)null);

        // Act
        var result = _controller.Authenticate(userDto);

        // Assert
        Assert.Null(result);
    }
}