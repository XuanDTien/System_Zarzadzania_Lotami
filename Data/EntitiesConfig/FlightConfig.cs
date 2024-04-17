using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Data.EntitiesConfig
{
    public class FlightConfig: IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.FlightNumber).IsRequired();
            builder.Property(f => f.DepartureDate).IsRequired();
            builder.HasOne(f => f.DepartureLocation)
                   .WithMany(l => l.DepartureFlights)
                   .HasForeignKey(f => f.DepartureFrom);
            builder.HasOne(f => f.ArrivalLocation)
                   .WithMany(l => l.ArrivalFlights)
                   .HasForeignKey(f => f.ArrivalTo);
            builder.HasOne(f => f.PlaneType)
                   .WithMany(p => p.Flights)
                   .HasForeignKey(f => f.PlaneTypeId);
        }
    }
}
