namespace SocialMediaRepositories.Dtos;

public class User
{
    public string Alias { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string ProfilePicture { get; set; }
    public string Description { get; set; }
    public int FollowerCount { get; set; }
}