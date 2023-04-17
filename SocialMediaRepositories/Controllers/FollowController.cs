using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowController : ControllerBase
{
    private readonly IFollowRepository _repository;

    public FollowController(IFollowRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Follow>>> GetFollows()
    {
        return Ok(await _repository.FollowsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Follow>> GetFollow(Guid id)
    {
        var follow = await _repository.GetByIdAsync(id);
        if (follow == null)
        {
            return NotFound();
        }
        return Ok(follow);
    }

    [HttpPost]
    public async Task<ActionResult<Follow>> CreateFollow(Follow follow)
    {
        await _repository.InsertOneAsync(follow);
        return Ok(await _repository.FollowsAsync());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFollow(Guid id)
    {
        var dbFollow = await _repository.GetByIdAsync(id);
        if (dbFollow == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(dbFollow);
        return Ok(await _repository.FollowsAsync());
    }
}