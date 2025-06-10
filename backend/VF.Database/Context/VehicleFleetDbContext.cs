using Microsoft.EntityFrameworkCore;
using VF.Database.Context.DbEntities;

namespace VF.Database.Context
{
    public class VehicleFleetDbContext : DbContext
    {
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<VehicleTypeEntity> VehicleTypes { get; set; }

        public VehicleFleetDbContext(DbContextOptions<VehicleFleetDbContext> options) 
            : base(options) { }
    }
}
