using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMediaRepositories.Dtos;
using SocialMediaRepositories.Helper;
using SocialMediaRepositories.Interfaces;
using User = SocialMediaRepositories.Models.User;

namespace SocialMediaRepositories.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public AccountController(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDto userDto)
    {
        var user = Authenticate(userDto);
        if (user != null)
        {
            var token = Generate(user);
            return Ok(new {token});
        }

        return NotFound("Wrong credentials");
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
    {
        PasswordHashing.CreatePasswordHash(userDto.Password, out var hash, out var salt);
        var user = new User
        {
            Alias = userDto.Alias,
            EmailAddress = userDto.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            Name = userDto.Name
        };

        await _repository.InsertOneAsync(user);
        return Ok("Registered User");
    }
    
    

    public string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name),
            new Claim(ClaimTypes.Surname, user.Alias),
            new Claim(ClaimTypes.Email, user.EmailAddress)
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"], 
            _configuration["Jwt:Audience"], 
            claims,
            expires: DateTime.Now.AddHours(5), 
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User Authenticate(UserDto userDto)
    {
        var currentUser = _repository.GetByAlias(userDto.Username);
        if (currentUser == null || !PasswordHashing.VerifyPasswordHash(userDto.Password, currentUser.PasswordHash, currentUser.PasswordSalt)) 
            currentUser = null;
        return currentUser!;
    }
}