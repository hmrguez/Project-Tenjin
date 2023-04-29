using Microsoft.AspNetCore.Mvc;
using SocialMediaRepositories.Helper;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Controllers;

public record UserResponse(string Alias, string Name, string EmailAddress, string? ProfilePicture, string Description);


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
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        return Ok(await GetAllUsersResponse());
    }
    
    [HttpGet("{alias}")]
    public async Task<ActionResult<User>> GetUser(string alias)
    {
        var temp = await _repository.GetByAliasAsync(alias);
        return Ok(new UserResponse(temp.Alias, temp.Name, temp.EmailAddress, temp.ProfilePicture!, temp.Description!));
    }
    
    [HttpPut]
    public async Task<ActionResult<List<User>>> UpdateUser(UserResponse user)
    {
        var dbUser = await _repository.GetByAliasAsync(user.Alias);
        if (dbUser == null)
            return NotFound();

        dbUser.Description = user.Description;
        dbUser.Name = user.Name;
        dbUser.ProfilePicture = user.ProfilePicture;
        dbUser.EmailAddress = user.EmailAddress;

        await _repository.UpdateOneAsync(dbUser);
        return Ok();
    }
    
    [HttpDelete("{alias}")]
    public async Task<ActionResult<List<User>>> DeleteUser(string alias)
    {
        var dbUser = await _repository.GetByAliasAsync(alias);
        if (dbUser == null)
            return NotFound();
        
        await _repository.DeleteAsync(dbUser);
        return Ok();
    }

    private async Task<IEnumerable<UserResponse>> GetAllUsersResponse()
    {
        var response = (await _repository.GetAllUsersAsync())
            .Select(x => new UserResponse(x.Alias, x.Name, x.EmailAddress, x.ProfilePicture!, x.Description!));
        return response;
    }
}