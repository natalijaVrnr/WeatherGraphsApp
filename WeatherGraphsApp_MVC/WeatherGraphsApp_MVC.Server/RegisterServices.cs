using Hangfire;
using Microsoft.EntityFrameworkCore;
using WeatherGraphsApp_MVC.Server.Data;
using WeatherGraphsApp_MVC.Server.Services;

namespace WeatherGraphsApp_MVC.Server
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddLogging();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddHangfire(config =>
            {
                config.UseInMemoryStorage();
            });
            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<IWeatherData, WeatherData>();
            builder.Services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
        }
    }
}
