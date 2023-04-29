using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DataContext _dbContext;

    public PostRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Post> Posts()
    {
        return _dbContext.Posts.ToList();
    }

    public async Task<IEnumerable<Post>> PostsAsync()
    {
        return await _dbContext.Posts.ToListAsync();
    }

    public Post GetById(Guid guid)
    {
        return _dbContext.Posts.FirstOrDefault(p => p.Id == guid);
    }

    public async Task<Post> GetByIdAsync(Guid guid)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == guid);
    }

    public async Task<Post> GetByUserAsync(string userAlias)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(p => p.UserAlias == userAlias);
    }

    public async Task InsertOneAsync(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOneAsync(Post post)
    {
        _dbContext.Posts.Update(post);
        await _dbContext.SaveChangesAsync();
    }
}