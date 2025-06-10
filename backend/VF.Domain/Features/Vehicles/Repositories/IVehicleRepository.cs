using VF.Domain.DataTypes;
using VF.Domain.Models;

namespace VF.Domain.Features.Vehicles.Repositories
{
    public interface IVehicleRepository
    {

        Task<Vehicle> FindAsync(ChassisId id, CancellationToken cancellationToken);

        Task<List<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);

        Task CreateAsync(Vehicle vehicle, CancellationToken cancellationToken);

        Task EditAsync(Vehicle vehicle, CancellationToken cancellationToken);

     

    }
}
