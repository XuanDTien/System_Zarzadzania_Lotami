using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data.Entities;
using System_Zarzadzania_Lotami.Data.EntitiesConfig;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Data
{
    public class FlightSystemContext(DbContextOptions<FlightSystemContext> options) : DbContext(options)
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PlaneType> Types { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new FlightConfig());
            modelBuilder.ApplyConfiguration(new LocationConfig());
            modelBuilder.ApplyConfiguration(new PlaneTypeConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            new SeedingData(modelBuilder).Seed();
        }
    }
}
