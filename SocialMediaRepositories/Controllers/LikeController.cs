using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeRepository _repository;

    public LikeController(ILikeRepository repository)
    {
        _repository = repository;
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
    public async Task<ActionResult<Like>> CreateLike(Like like)
    {
        await _repository.InsertOneAsync(like);
        return Ok(await _repository.LikesAsync());
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

        return Ok(await _repository.LikesAsync());
    }
}