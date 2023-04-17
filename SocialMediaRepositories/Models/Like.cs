using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaRepositories.Models;

public class Like
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post? Post { get; set; }

    // [ForeignKey("User")]
    public string UserAlias { get; set; }
    // public User User { get; set; }
    
    public DateTime DateLiked { get; set; }
}