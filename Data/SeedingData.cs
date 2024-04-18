using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System_Zarzadzania_Lotami.Data.Entities;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Data
{
    public class SeedingData(ModelBuilder modelBuilder)
    {
        private readonly ModelBuilder _modelBuilder = modelBuilder;

        public void Seed()
        {
            _modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, CityName = "New York" },
            new Location { Id = 2, CityName = "Warsaw" },
            new Location { Id = 3, CityName = "Gdansk" },
            new Location { Id = 4, CityName = "Krakow" }
            );

            _modelBuilder.Entity<PlaneType>().HasData(
            new PlaneType { Id = 1, TypeName = "Boeing" },
            new PlaneType { Id = 2, TypeName = "Airbus" },
            new PlaneType { Id = 3, TypeName = "Embraer"}
            );

            _modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 1,
                FlightNumber = 4534,
                DepartureDate = new DateTime(2024, 4, 20, 8, 30, 0),
                DepartureFrom = 4,
                ArrivalTo = 2,
                PlaneTypeId = 3
            },
            new Flight
            {
                Id = 2,
                FlightNumber = 2432,
                DepartureDate = new DateTime(2024, 4, 22, 18, 0, 0),
                DepartureFrom = 3,
                ArrivalTo = 1,
                PlaneTypeId = 1
            },
            new Flight
            {
                Id = 3,
                FlightNumber = 3464,
                DepartureDate = new DateTime(2024, 4, 21, 4, 45, 0),
                DepartureFrom = 1,
                ArrivalTo = 2,
                PlaneTypeId = 2
            }
           );

            _modelBuilder.Entity<User>().HasData(
             new User { Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin", 13) }
             );

        }
    }
}
