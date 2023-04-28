using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;


public record LikeRequest(string UserAlias, Guid PostId);

[Route("api/[controller]")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeRepository _repository;
    private readonly IPostRepository _postRepository;

    public LikeController(ILikeRepository repository, IPostRepository postRepository)
    {
        _repository = repository;
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Like>>> GetLikes()
    {
        return Ok(await _repository.LikesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Like>> GetLike(Guid id)
    {
        var like = await _repository.GetByIdAsync(id);
        if (like == null)
        {
            return NotFound();
        }
        return Ok(like);
    }

    [HttpPost]
    public async Task<ActionResult<Like>> CreateLike(LikeRequest like)
    {
        var newLike = new Like
        {
            PostId = like.PostId,
            UserAlias = like.UserAlias
        };
        var post = await _postRepository.GetByIdAsync(like.PostId);
        if (post == null)
            return NotFound();
        
        post.LikeCount++;
        await _postRepository.UpdateOneAsync(post);
        await _repository.InsertOneAsync(newLike);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(Guid id)
    {
        var dbLike = await _repository.GetByIdAsync(id);
        if (dbLike == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(dbLike);

        return Ok();
    }
}