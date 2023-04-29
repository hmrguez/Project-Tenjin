using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaRepositories.Models;

public class Post
{
    public Guid Id { get; set; }
    public int LikeCount { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Picture { get; set; }
    public string? Text { get; set; }
    public string UserAlias { get; set; }
}