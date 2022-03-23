

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Data.Mapping
{
   public class TruckMapping : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.HasKey(u => u.Id);

            builder.ToTable("Trucks");

            builder.Property(u => u.Model).IsRequired().HasColumnType($"varchar(300)");            

            builder.Property(u => u.ManufactureYear).IsRequired();

            builder.Property(u => u.ModelYear).IsRequired();

        }
    }
}
