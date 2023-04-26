using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaRepositories.Models;

public class Follow
{
    [Key]
    [Required(AllowEmptyStrings = true)]
    public Guid Id { get; set; }
    
    public DateTime DateStarted { get; set; }

    // [ForeignKey("User")]
    public string FollowerAlias { get; set; }
    // public User Follower { get; set; }

    // [ForeignKey("User")]
    public string FollowedAlias { get; set; }
    // public User Followed { get; set; }
}