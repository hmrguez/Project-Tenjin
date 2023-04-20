using System.ComponentModel.DataAnnotations;

namespace SocialMediaRepositories.Models;

public class User
{
    [Key]
    public string Alias { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    
    // Password
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public int FollowerCount { get; set; }
}