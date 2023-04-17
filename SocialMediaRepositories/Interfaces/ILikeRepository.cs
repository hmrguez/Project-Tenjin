using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Interfaces;

public interface ILikeRepository
{
    // Gets
    public IEnumerable<Like> Likes();
    public Task<IEnumerable<Like>> LikesAsync();
    public Like GetById(Guid guid);
    public Task<Like> GetByIdAsync(Guid guid);
    
    public Task<Like> GetByPostAsync(Guid postId);
    public Task<Like> GetByUserAsync(string userAlias);
    
    // Posts
    public Task InsertOneAsync(Like like);
    
    // DELETE
    public Task DeleteAsync(Like like);
    
    // PUT
    public Task UpdateOneAsync(Like like);
}