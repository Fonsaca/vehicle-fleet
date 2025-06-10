using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VF.Database.Context.DbEntities;

namespace VF.Database.Context.EntityMap
{
    internal class VehicleTypeEntityMap : IEntityTypeConfiguration<VehicleTypeEntity>
    {
        public void Configure(EntityTypeBuilder<VehicleTypeEntity> builder)
        {

            builder.HasKey(k => k.Code);
            builder.Property(p => p.Code).HasMaxLength(20);
            builder.ToTable("VehicleType");

            builder.HasData([
                new VehicleTypeEntity(){ Code = "Truck"},
                new VehicleTypeEntity(){ Code = "Bus"},
                new VehicleTypeEntity(){ Code = "Car"}
                ]);
        }
    }
}
