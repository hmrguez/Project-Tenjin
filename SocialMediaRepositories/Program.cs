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
    builder.Services.AddCors(options => options.AddPolicy(name: "SocialMediaOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
}

var app = builder.Build();
{
    app.UseCors("SocialMediaOrigins");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

