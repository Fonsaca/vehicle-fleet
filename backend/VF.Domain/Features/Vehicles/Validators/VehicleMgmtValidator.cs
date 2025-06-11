using FluentValidation;
using VF.Domain.DataTypes;
using VF.Domain.Features.Vehicles.Repositories;
using VF.Domain.Models;

namespace VF.Domain.Features.Vehicles.Validators
{
    public class VehicleMgmtValidator : AbstractValidator<Vehicle>
    {

        private readonly IVehicleRepository _repository;

        public VehicleMgmtValidator(IVehicleRepository repository)
        {
            _repository = repository;

            RuleFor(r => r.ChassisId)
                .Must(id => !id.Equals(new ChassisId())).WithMessage("Chassis Id must be informed")
                .MustAsync(MustBeUnique).WithMessage("Chassis Id must be unique");

            RuleFor(r => r.Type).NotEmpty().WithMessage("Vehicle type must be informed");
            RuleFor(r => r.Color).NotEmpty().WithMessage("Color must be informed");
            
        }


        private async Task<bool> MustBeUnique(ChassisId id, CancellationToken cancellationToken)
        {
            return (await _repository.FindAsync(id, cancellationToken)) == default;
        }
    }
}
