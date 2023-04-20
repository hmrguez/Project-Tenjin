namespace SocialMediaRepositories.Dtos;

public class CreateUserDto
{
    public string Alias { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; }= "";
    public string Password { get; set; }= "";
}