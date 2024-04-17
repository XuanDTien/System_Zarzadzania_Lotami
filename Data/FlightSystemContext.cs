using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data.EntitiesConfig;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Data
{
    public class FlightSystemContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PlaneType> Types { get; set; }

        public FlightSystemContext(DbContextOptions<FlightSystemContext> options) : base(options) { }

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FlightDataBase");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new FlightConfig());
            modelBuilder.ApplyConfiguration(new LocationConfig());
            modelBuilder.ApplyConfiguration(new PlaneTypeConfig());

            new SeedingData(modelBuilder).Seed();
        }
    }
}
