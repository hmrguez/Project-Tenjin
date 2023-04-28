using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;

public record PostResponse(Guid Guid, int LikeCount, DateTime DateCreated, string? Picture, string? Text, string UserAlias);

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
        var posts = (await _repository.PostsAsync())
            .Select(x=> new PostResponse(x.Id, x.LikeCount, x.DateCreated, x.Picture, x.Text, x.UserAlias));
        return Ok(posts);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(Guid id)
    {
        var temp = await _repository.GetByIdAsync(id);
        return Ok(new PostResponse(temp.Id, temp.LikeCount, temp.DateCreated, temp.Picture, temp.Text, temp.UserAlias));
    }

    [HttpPost]
    public async Task<ActionResult<List<Post>>> CreatePost(Post post)
    {
        post.Id = new Guid();
        await _repository.InsertOneAsync(post);
        return Ok();
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
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var dbPost = await _repository.GetByIdAsync(id);
        if (dbPost == null)
            return NotFound();

        await _repository.DeleteAsync(dbPost);
        return Ok();
    }
}