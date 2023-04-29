using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SocialMediaModels;
using SocialMediaRepositories.Controllers;
using SocialMediaRepositories.Interfaces;

namespace SocialMediaApiTesting.ControllerTests;

public class CommentControllerTests
{
    private readonly CommentController _controller;
    private readonly Mock<ICommentRepository> _mockRepository;

    public CommentControllerTests()
    {
        _mockRepository = new Mock<ICommentRepository>();
        _controller = new CommentController(_mockRepository.Object);
    }

    [Fact]
    public async Task GetComments_ReturnsOkObjectResult_WithListOfComments()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new() { Id = Guid.NewGuid(), Text = "Comment 1" },
            new() { Id = Guid.NewGuid(), Text = "Comment 2" }
        };
        _mockRepository.Setup(repo => repo.CommentsAsync()).ReturnsAsync(comments);

        // Act
        var result = await _controller.GetComments();

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeOfType<List<Comment>>();
        okResult?.Value.Should().BeEquivalentTo(comments);
    }

    [Fact]
    public async Task GetComment_WithValidId_ReturnsOkObjectResult_WithComment()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Comment { Id = commentId, Text = "Test Comment" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(comment);

        // Act
        var result = await _controller.GetComment(commentId);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeOfType<Comment>();
        okResult?.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task GetComment_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(null as Comment);

        // Act
        var result = await _controller.GetComment(commentId);
        var okResult = result.Result as NotFoundResult;

        // Assert
        okResult.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateComment_ReturnsOkObjectResult_WithListOfComments()
    {
        // Arrange
        var commentRequest = new CommentController.CommentRequest(Guid.NewGuid(), "testuser", "test comment");
        var comment = new Comment
        {
            Id = new Guid(),
            PostId = commentRequest.PostId,
            Text = commentRequest.CommentText,
            UserAlias = commentRequest.UserAlias
        };
        
        // Act
        var result = await _controller.CreateComment(commentRequest);
        var okResult = result.Result as OkResult;
    
        // Assert
        okResult.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateComment_WithValidComment_ReturnsOkObjectResult_WithListOfComments()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var dbComment = new Comment { Id = commentId, Text = "Old Text" };
        var comment = new Comment { Id = commentId, Text = "New Text" };
        var comments = new List<Comment> { comment };
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(dbComment);
        _mockRepository.Setup(repo => repo.CommentsAsync()).ReturnsAsync(comments);
        
        
        // Act
        var result = await _controller.UpdateComment(comment);
        var okResult = result.Result as OkObjectResult;
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<List<Comment>>().And.BeEquivalentTo(comments);
        
        _mockRepository.Verify(repo => repo.UpdateOneAsync(dbComment), Times.Once);
    }

    [Fact]
    public async Task UpdateComment_WithInvalidComment_ReturnsNotFoundResult()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Comment { Id = commentId, Text = "New Text" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(null as Comment);

        // Act
        var result = await _controller.UpdateComment(comment);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteComment_WithValidId_ReturnsOkObjectResult_WithListOfComments()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var dbComment = new Comment { Id = commentId, Text = "Comment" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(dbComment);
        _mockRepository.Setup(repo => repo.CommentsAsync()).ReturnsAsync(new List<Comment>());
        
        // Act
        var result = await _controller.DeleteComment(commentId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(new List<Comment>());
        _mockRepository.Verify(repo => repo.DeleteAsync(dbComment), Times.Once);
    }

    [Fact]
    public async Task DeleteComment_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetByIdAsync(commentId)).ReturnsAsync(null as Comment);

        // Act
        var result = await _controller.DeleteComment(commentId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }
}