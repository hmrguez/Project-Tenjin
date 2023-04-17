using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly DataContext _dbContext;

    public FollowRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Follow> Follows()
    {
        return _dbContext.Follows.ToList();
    }

    public async Task<IEnumerable<Follow>> FollowsAsync()
    {
        return await _dbContext.Follows.ToListAsync();
    }

    public Follow GetById(Guid guid)
    {
        return _dbContext.Follows.FirstOrDefault(f => f.Id == guid);
    }

    public async Task<Follow> GetByIdAsync(Guid guid)
    {
        return await _dbContext.Follows.FirstOrDefaultAsync(f => f.Id == guid);
    }

    public async Task<Follow> GetByFollowerAsync(string followerAlias)
    {
        return await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerAlias == followerAlias);
    }

    public async Task<Follow> GetByFollowedAsync(string followedAlias)
    {
        return await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowedAlias == followedAlias);
    }

    public async Task InsertOneAsync(Follow follow)
    {
        await _dbContext.Follows.AddAsync(follow);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Follow follow)
    {
        _dbContext.Follows.Remove(follow);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOneAsync(Follow follow)
    {
        _dbContext.Follows.Update(follow);
        await _dbContext.SaveChangesAsync();
    }
}