namespace VF.Domain.Models
{
    public class Bus : Vehicle
    {
        public override string Type => "Bus";

        public override ushort NumberOfPassengers => 42;
    }
}
