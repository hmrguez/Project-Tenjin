using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Interfaces;

public interface IUserRepository
{
    // Gets
    public IEnumerable<User> GetAllUsers();
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public User GetByAlias(string alias);
    public Task<User> GetByAliasAsync(string alias);
    public Task<User> GetByEmailAsync(string email);
    
    // Posts
    public Task InsertOneAsync(User user);
    
    // DELETE
    public Task DeleteAsync(User user);
    
    // PUT
    public Task UpdateOneAsync(User user);
}