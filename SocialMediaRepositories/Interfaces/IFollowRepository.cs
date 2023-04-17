using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Interfaces;

public interface IFollowRepository
{
    // Gets
    public IEnumerable<Follow> Follows();
    public Task<IEnumerable<Follow>> FollowsAsync();
    public Follow GetById(Guid guid);
    public Task<Follow> GetByIdAsync(Guid guid);
    
    public Task<Follow> GetByFollowerAsync(string followerAlias);
    public Task<Follow> GetByFollowedAsync(string followedAlias);
    
    // Posts
    public Task InsertOneAsync(Follow follow);
    
    // DELETE
    public Task DeleteAsync(Follow follow);
    
    // PUT
    public Task UpdateOneAsync(Follow follow);
}