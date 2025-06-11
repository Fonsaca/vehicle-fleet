using Microsoft.EntityFrameworkCore;
using VF.Database.Context.DbEntities;
using VF.Database.Context.EntityMap;

namespace VF.Database.Context
{
    public class VehicleFleetDbContext : DbContext
    {
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<VehicleTypeEntity> VehicleTypes { get; set; }

        public VehicleFleetDbContext(DbContextOptions<VehicleFleetDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleTypeEntityMap());
            modelBuilder.ApplyConfiguration(new VehicleEntityMap());
        }
    }
}
