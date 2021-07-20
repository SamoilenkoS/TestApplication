using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EFCoreContext : DbContext
    {
        public DbSet<WeatherForecastDTO> WeatherForecasts { get; set;}

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
