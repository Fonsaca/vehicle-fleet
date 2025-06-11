using VF.Domain.DataTypes;

namespace VF.Domain.Models
{
    public abstract class Vehicle
    {

        public ChassisId ChassisId { get; set; }

        public string Color { get; set; }

        public abstract string Type { get; }

        public abstract ushort NumberOfPassengers { get; }



    }
}
