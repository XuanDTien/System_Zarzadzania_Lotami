using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data.Entity;
using System_Zarzadzania_Lotami.Data.Entities;

namespace System_Zarzadzania_Lotami.Data.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Username);
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
