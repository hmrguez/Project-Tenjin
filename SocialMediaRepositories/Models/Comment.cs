using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialMediaRepositories.Models;

namespace SocialMediaModels;

public class Comment
{
    [Key]
    [Required(AllowEmptyStrings = true)]
    public Guid Id { get; set; }
    
    [Required]
    public string UserAlias { get; set; }

    [Required]
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post? Post { get; set; }
    
    [Required]
    public string Text { get; set; }
}