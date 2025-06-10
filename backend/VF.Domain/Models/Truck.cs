namespace VF.Domain.Models
{
    public class Truck : Vehicle
    {
        public override string Type => "Truck";

        public override ushort NumberOfPassengers => 1;
    }
}
