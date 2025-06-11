using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VF.Database.Context.DbEntities;

namespace VF.Database.Context.EntityMap
{
    internal class VehicleEntityMap : IEntityTypeConfiguration<VehicleEntity>
    {
        public void Configure(EntityTypeBuilder<VehicleEntity> builder)
        {

            builder.ToTable("Vehicle");
            builder.HasKey(k => new { k.ChassisSerie, k.ChassisNumber });
            builder.Property(p => p.Color).IsRequired();

            builder.HasOne(o => o.VehicleType)
                .WithMany()
                .HasForeignKey(fk => fk.Type);

            builder.HasData([
                    new VehicleEntity(){
                        Type = "Bus",
                        ChassisNumber = 1,
                        ChassisSerie = "B0001",
                        Color = "Red"
                    }
                ]);
        }
    }
}
