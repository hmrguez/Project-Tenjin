using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialMediaRepositories.Models;

namespace SocialMediaModels;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    // [ForeignKey("User")]
    public string UserAlias { get; set; }
    // public User User { get; set; }

    [Required]
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    
    [Required]
    public string Text { get; set; }
}