using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Interfaces;

public interface IPostRepository
{
    // Gets
    public IEnumerable<Post> Posts();
    public Task<IEnumerable<Post>> PostsAsync();
    public Post GetById(Guid guid);
    public Task<Post> GetByIdAsync(Guid guid);
    
    public Task<Post> GetByUserAsync(string userAlias);
    
    // Posts
    public Task InsertOneAsync(Post like);
    
    // DELETE
    public Task DeleteAsync(Post like);
    
    // PUT
    public Task UpdateOneAsync(Post like);
}