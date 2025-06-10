using VF.Domain.DataTypes;

namespace VF.Application.Features.Vehicles.Dtos
{
    public class VehicleEditDto
    {
        public ChassisId ChassisId { get; set; }

        public string Color { get; set; } = string.Empty;
    }
}
