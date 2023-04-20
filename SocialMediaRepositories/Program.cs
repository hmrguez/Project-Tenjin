using SocialMediaRepositories;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
});

await builder.Build().RunAsync();