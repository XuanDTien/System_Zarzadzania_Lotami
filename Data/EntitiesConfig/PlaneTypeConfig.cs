using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Data.EntitiesConfig
{
    public class PlaneTypeConfig : IEntityTypeConfiguration<PlaneType>
    {
        public void Configure(EntityTypeBuilder<PlaneType> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TypeName).IsRequired().HasMaxLength(50);
        }
    }
}
