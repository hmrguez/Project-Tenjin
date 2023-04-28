using Microsoft.AspNetCore.Mvc;
using SocialMediaModels;
using SocialMediaRepositories.Interfaces;

namespace SocialMediaRepositories.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _repository;

    public record CommentRequest(Guid PostId, string UserAlias, string CommentText);
    
    public CommentController(ICommentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Comment>>> GetComments()
    {
        return Ok(await _repository.CommentsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(Guid id)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult<List<Comment>>> CreateComment(CommentRequest request)
    {
        var comment = new Comment()
        {
            PostId = request.PostId,
            Text = request.CommentText,
            UserAlias = request.UserAlias
        };
        await _repository.InsertOneAsync(comment);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<List<Comment>>> UpdateComment(Comment comment)
    {
        var dbComment = await _repository.GetByIdAsync(comment.Id);
        if (dbComment == null)
        {
            return NotFound();
        }

        dbComment.Text = comment.Text;

        await _repository.UpdateOneAsync(dbComment);

        return Ok(await _repository.CommentsAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Comment>>> DeleteComment(Guid id)
    {
        var dbComment = await _repository.GetByIdAsync(id);
        if (dbComment == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(dbComment);

        return Ok(await _repository.CommentsAsync());
    }
}