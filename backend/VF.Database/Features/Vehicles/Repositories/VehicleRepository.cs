using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VF.Database.Context;
using VF.Database.Context.DbEntities;
using VF.Domain.DataTypes;
using VF.Domain.Features.Vehicles.Repositories;
using VF.Domain.Models;

namespace VF.Database.Features.Vehicles.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly VehicleFleetDbContext _context;

        private readonly IMapper _autoMapper;

        public VehicleRepository(VehicleFleetDbContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        public async Task CreateAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            var dbVehicle = _autoMapper.Map<VehicleEntity>(vehicle);

            await _context.Vehicles.AddAsync(dbVehicle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);    
        }

        public async Task EditAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            var vehicleToUpdate = _autoMapper.Map<VehicleEntity>(vehicle);

            var vehicleDb = await _context.Vehicles.FindAsync(vehicle.ChassisId, cancellationToken);

            if (vehicleDb == default)
                throw new KeyNotFoundException("Vehicle not found");

            vehicleDb.Color = vehicleToUpdate.Color;

            _context.Entry(vehicleDb).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Vehicle> FindAsync(ChassisId id, CancellationToken cancellationToken)
        {
            var vehicle  = await _context.Vehicles.FindAsync(id, cancellationToken);

            return _autoMapper.Map<Vehicle>(vehicle);
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.ToListAsync(cancellationToken);

            return _autoMapper.Map<List<Vehicle>>(vehicle);
        }
    }
}
