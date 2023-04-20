// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using SocialMediaRepositories.Controllers;
// using SocialMediaRepositories.Interfaces;
// using SocialMediaRepositories.Models;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Xunit;
//
// namespace SocialMediaApiTesting.ControllerTests
// {
//     public class UserControllerTests
//     {
//         private readonly UserController _controller;
//         private readonly Mock<IUserRepository> _mockRepository;
//
//         public UserControllerTests()
//         {
//             _mockRepository = new Mock<IUserRepository>();
//             _controller = new UserController(_mockRepository.Object);
//         }
//
//         [Fact]
//         public async Task GetUsers_ReturnsOkObjectResult_WithListOfUsers()
//         {
//             // Arrange
//             var users = new List<User>
//             {
//                 new() { Alias = "user1", Name = "User 1", Description = "User 1 description", FollowerCount = 10, ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com", Password = "password1" },
//                 new() { Alias = "user2", Name = "User 2", Description = "User 2 description", FollowerCount = 20, ProfilePicture = "profile2.jpg", EmailAddress = "user2@example.com", Password = "password2" },
//             };
//             _mockRepository.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);
//
//             // Act
//             var result = await _controller.GetUsers();
//
//             // Assert
//             var okObjectResult = result.Result as OkObjectResult;
//             okObjectResult.Should().NotBeNull();
//             okObjectResult?.Value.Should().BeEquivalentTo(users);
//         }
//
//         [Fact]
//         public async Task GetUser_ReturnsOkObjectResult_WithUser()
//         {
//             // Arrange
//             var alias = "user1";
//             var user = new User { Alias = alias, Name = "User 1", Description = "User 1 description", FollowerCount = 10, ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com", Password = "password1" };
//             _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(user);
//
//             // Act
//             var result = await _controller.GetUser(alias);
//
//             // Assert
//             var okObjectResult = result.Result as OkObjectResult;
//             okObjectResult.Should().NotBeNull();
//             okObjectResult?.Value.Should().BeEquivalentTo(user);
//         }
//
//         [Fact]
//         public async Task CreateUser_ReturnsOkObjectResult_WithListOfUsers()
//         {
//             // Arrange
//             var user = new User { Alias = "user1", Name = "User 1", Description = "User 1 description", FollowerCount = 10, ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com", Password = "password1" };
//             _mockRepository.Setup(x => x.InsertOneAsync(user));
//
//             // Act
//             var result = await _controller.CreateUser(user);
//
//             // Assert
//             var okObjectResult = result.Result as OkObjectResult;
//             okObjectResult.Should().NotBeNull();
//             okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.GetAllUsersAsync());
//         }
//
//         [Fact]
//         public async Task UpdateUser_ReturnsOkObjectResult_WithListOfUsers()
//         {
//             // Arrange
//             var alias = "user1";
//             var user = new User { Alias = alias, Name = "User 1 updated", Description = "User 1 updated description", FollowerCount = 20, ProfilePicture = "profile1-updated.jpg", EmailAddress = "user1-updated@example.com", Password = "password1-updated" };
//             var dbUser = new User { Alias = alias, Name = "User 1", Description = "User 1 description", FollowerCount = 10, ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com", Password = "password1" };
//             _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(dbUser);
//
//             // Act
//             var result = await _controller.UpdateUser(user);
//
//             // Assert
//             var okObjectResult = result.Result as OkObjectResult;
//             okObjectResult.Should().NotBeNull();
//             okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.GetAllUsersAsync());
//         }
//
//         [Fact]
//         public async Task DeleteUser_ReturnsOkObjectResult_WithListOfUsers()
//         {
//             // Arrange
//             var alias = "user1";
//             var user = new User { Alias = alias, Name = "User 1", Description = "User 1 description", FollowerCount = 10, ProfilePicture = "profile1.jpg", EmailAddress = "user1@example.com", Password = "password1" };
//             _mockRepository.Setup(x => x.GetByAliasAsync(alias)).ReturnsAsync(user);
//             _mockRepository.Setup(x => x.DeleteAsync(user));
//
//             // Act
//             var result = await _controller.DeleteUser(alias);
//
//             // Assert
//             var okObjectResult = result.Result as OkObjectResult;
//             okObjectResult.Should().NotBeNull();
//             okObjectResult?.Value.Should().BeEquivalentTo(await _mockRepository.Object.GetAllUsersAsync());
//         }
//     }
// }