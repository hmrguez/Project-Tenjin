using SocialMediaModels;

namespace SocialMediaRepositories.Interfaces;

public interface ICommentRepository
{
    // Gets
    public IEnumerable<Comment> Comments();
    public Task<IEnumerable<Comment>> CommentsAsync();
    public Comment GetById(Guid guid);
    public Task<Comment> GetByIdAsync(Guid guid);
    
    public Task<Comment> GetByPostAsync(Guid postId);
    public Task<Comment> GetByUserAsync(string userAlias);
    
    // Posts
    public Task InsertOneAsync(Comment like);
    
    // DELETE
    public Task DeleteAsync(Comment like);
    
    // PUT
    public Task UpdateOneAsync(Comment like);
}