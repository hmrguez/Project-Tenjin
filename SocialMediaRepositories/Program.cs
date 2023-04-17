using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    builder.Services.AddScoped<IPostRepository, PostRepository>();
    builder.Services.AddScoped<IFollowRepository, FollowRepository>();
    builder.Services.AddScoped<ILikeRepository, LikeRepository>();
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

