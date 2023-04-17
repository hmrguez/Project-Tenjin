using Microsoft.EntityFrameworkCore;
using SocialMediaModels;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;

namespace SocialMediaRepositories.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DataContext _dbContext;

    public CommentRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Comment> Comments()
    {
        return _dbContext.Comments.ToList();
    }

    public async Task<IEnumerable<Comment>> CommentsAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public Comment GetById(Guid guid)
    {
        return _dbContext.Comments.FirstOrDefault(c => c.Id == guid);
    }

    public async Task<Comment> GetByIdAsync(Guid guid)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == guid);
    }

    public async Task<Comment> GetByPostAsync(Guid postId)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.PostId == postId);
    }

    public async Task<Comment> GetByUserAsync(string userAlias)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.UserAlias == userAlias);
    }

    public async Task InsertOneAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOneAsync(Comment comment)
    {
        _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync();
    }
}