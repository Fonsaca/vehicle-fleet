using VF.Application.Features.Vehicles.Dtos;
using VF.Domain.DataTypes;
using VF.Domain.Models;

namespace VF.Application.Features.Vehicles.Applications
{
    public interface IVehicleApplication
    {

        Task<List<VehicleDto>> GetVehiclesAsync(CancellationToken cancellationToken);
        
        Task<VehicleDto?> FindAsync(ChassisId chassisId, CancellationToken cancellationToken);

        Task CreateAsync(VehicleCreateDto vehicleToCreate , CancellationToken cancellationToken);
        
        Task EditAsync(VehicleEditDto vehicleToEdit, CancellationToken cancellationToken);
        
       

    }
}
