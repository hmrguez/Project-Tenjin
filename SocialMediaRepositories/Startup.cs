using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;
using SocialMediaRepositories.Interfaces;
using SocialMediaRepositories.Repositories;

namespace SocialMediaRepositories;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
            
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
            
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
            
        services.AddCors(options => options.AddPolicy(name: "SocialMediaOrigins", policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
        }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors("SocialMediaOrigins");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}