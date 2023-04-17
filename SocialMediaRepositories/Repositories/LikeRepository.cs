using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly DataContext _dbContext;

    public LikeRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Like> Likes()
    {
        return _dbContext.Likes.ToList();
    }

    public async Task<IEnumerable<Like>> LikesAsync()
    {
        return await _dbContext.Likes.ToListAsync();
    }

    public Like GetById(Guid guid)
    {
        return _dbContext.Likes.FirstOrDefault(l => l.Id == guid);
    }

    public async Task<Like> GetByIdAsync(Guid guid)
    {
        return await _dbContext.Likes.FirstOrDefaultAsync(l => l.Id == guid);
    }

    public async Task<Like> GetByPostAsync(Guid postId)
    {
        return await _dbContext.Likes.FirstOrDefaultAsync(l => l.PostId == postId);
    }

    public async Task<Like> GetByUserAsync(string userAlias)
    {
        return await _dbContext.Likes.FirstOrDefaultAsync(l => l.UserAlias == userAlias);
    }

    public async Task InsertOneAsync(Like like)
    {
        await _dbContext.Likes.AddAsync(like);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Like like)
    {
        _dbContext.Likes.Remove(like);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOneAsync(Like like)
    {
        _dbContext.Likes.Update(like);
        await _dbContext.SaveChangesAsync();
    }
}