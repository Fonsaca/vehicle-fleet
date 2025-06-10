using AutoMapper;
using VF.Application.Features.Vehicles.Dtos;
using VF.Domain.DataTypes;
using VF.Domain.Exceptions;
using VF.Domain.Features.Vehicles.Repositories;
using VF.Domain.Features.Vehicles.Validators;
using VF.Domain.Models;

namespace VF.Application.Features.Vehicles.Applications.Src
{
    public class VehicleApplication : IVehicleApplication
    {

        private readonly IVehicleRepository _repository;

        private readonly IMapper _autoMapper;

        public VehicleApplication(IVehicleRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }


        public async Task CreateAsync(VehicleCreateDto vehicleToCreate, CancellationToken cancellationToken)
        {
            var vehicle = _autoMapper.Map<Vehicle>(vehicleToCreate);

            var validator = new VehicleMgmtValidator(_repository);
            var validationResult = await validator.ValidateAsync(vehicle, cancellationToken);

            if (!validationResult.IsValid)
                throw new VehicleValidationException("Create vehicle error", validationResult.Errors);

            await _repository.CreateAsync(vehicle, cancellationToken);
        }


        public async Task EditAsync(VehicleEditDto vehicleToEdit, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.FindAsync(vehicleToEdit.ChassisId, cancellationToken);

            if (vehicle == default)
                throw new KeyNotFoundException("Vehicle not found");

            vehicle.Color = vehicleToEdit.Color;
            
            await _repository.EditAsync(vehicle, cancellationToken);
        }

        public async Task<VehicleDto?> FindAsync(ChassisId chassisId, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.FindAsync(chassisId, cancellationToken);
            if (vehicle == default)
                return default;
            return _autoMapper.Map<VehicleDto>(vehicle);
        }

        public async Task<List<VehicleDto>> GetVehiclesAsync(CancellationToken cancellationToken)
        {
            var vehicles = await _repository.GetVehiclesAsync(cancellationToken);
            return _autoMapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
