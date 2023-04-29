using System.Data;
using System.Data.SqlClient;
using Dapper;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Models;

namespace SocialMediaRepositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public IEnumerable<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _db.QueryAsync<User>("select * from Users");
    }

    public User GetByAlias(string alias)
    {
        return _db.QueryFirstOrDefault<User>("select * from Users where Alias = @Alias", new { Alias = alias });
    }

    public async Task<User> GetByAliasAsync(string alias)
    {
        return await _db.QueryFirstOrDefaultAsync<User>("select * from Users where Alias = @Alias", new { Alias = alias });
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
        // return await _dbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);
    }

    public async Task InsertOneAsync(User user)
    {
        await _db.ExecuteAsync(@"INSERT INTO Users (Alias, Name, EmailAddress, PasswordHash, PasswordSalt, ProfilePicture, Description, FollowerCount)
        VALUES (@Alias, @Name, @EmailAddress, @PasswordHash, @PasswordSalt, @ProfilePicture, @Description, @FollowerCount);", user);
        // await _dbContext.Users.AddAsync(user);
        // await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        var sql = "DELETE FROM Users WHERE Alias = @Alias";
        await _db.ExecuteAsync(sql, user);
    }

    public async Task UpdateOneAsync(User user)
    {
        var sql = @"UPDATE Users
                SET Name = @Name,
                    EmailAddress = @EmailAddress,
                    PasswordHash = @PasswordHash,
                    PasswordSalt = @PasswordSalt,
                    ProfilePicture = @ProfilePicture,
                    Description = @Description,
                    FollowerCount = @FollowerCount
                WHERE Alias = @Alias";
        await _db.ExecuteAsync(sql, user);
    }
}