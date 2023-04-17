using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return Ok(await _repository.GetAllUsersAsync());
    }
    
    [HttpGet("{alias}")]
    public async Task<ActionResult<User>> GetUsers(string alias)
    {
        return Ok(await _repository.GetByAliasAsync(alias));
    }

    [HttpPost]
    public async Task<ActionResult<List<User>>> CreateUser(User user)
    {
        await _repository.InsertOneAsync(user);
        return Ok(await _repository.GetAllUsersAsync());
    }
    
    [HttpPut]
    public async Task<ActionResult<List<User>>> UpdateUser(User user)
    {
        var dbUser = await _repository.GetByAliasAsync(user.Alias);
        if (dbUser == null)
            return NotFound();

        dbUser.Description = user.Description;
        dbUser.Name = user.Name;
        dbUser.FollowerCount = user.FollowerCount;
        dbUser.ProfilePicture = user.ProfilePicture;
        dbUser.EmailAddress = user.EmailAddress;
        dbUser.Password = user.Password;

        await _repository.UpdateOneAsync(dbUser);

        return Ok(await _repository.GetAllUsersAsync());
    }
    
    [HttpDelete("{alias}")]
    public async Task<ActionResult<List<User>>> DeleteUser(string alias)
    {
        var dbUser = await _repository.GetByAliasAsync(alias);
        if (dbUser == null)
            return NotFound();
        
        await _repository.DeleteAsync(dbUser);
        return Ok(await _repository.GetAllUsersAsync());
    }
}