using Microsoft.EntityFrameworkCore;

namespace WeatherGraphsApp_MVC.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherModel> Weather { get; set; }
    }
}
