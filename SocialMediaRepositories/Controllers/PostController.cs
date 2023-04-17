using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostRepository _repository;

    public PostController(IPostRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        return Ok(await _repository.PostsAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPosts(Guid id)
    {
        return Ok(await _repository.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<List<Post>>> CreatePost(Post post)
    {
        await _repository.InsertOneAsync(post);
        return Ok(await _repository.PostsAsync());
    }
    
    [HttpPut]
    public async Task<ActionResult<List<Post>>> UpdatePost(Post post)
    {
        var dbPost = await _repository.GetByIdAsync(post.Id);
        if (dbPost == null)
            return NotFound();

        dbPost.LikeCount = post.LikeCount;
        dbPost.Picture = post.Picture;
        dbPost.Text = post.Text;

        await _repository.UpdateOneAsync(dbPost);
        return Ok(await _repository.PostsAsync());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var dbPost = await _repository.GetByIdAsync(id);
        if (dbPost == null)
            return NotFound();

        await _repository.DeleteAsync(dbPost);
        return Ok(await _repository.PostsAsync());
    }
}