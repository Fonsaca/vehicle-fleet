using VF.Domain.DataTypes;

namespace VF.Application.Features.Vehicles.Dtos
{
    public class VehicleDto
    {
        public ChassisId ChassisId { get; set; }

        public string Color { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public ushort NumberOfPassengers { get; set; } = 0;
    }
}
