using Microsoft.EntityFrameworkCore;
using SocialMediaRepositories.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
}

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

