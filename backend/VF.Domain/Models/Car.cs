namespace VF.Domain.Models
{
    public class Car : Vehicle
    {
        public override string Type => "Car";

        public override ushort NumberOfPassengers => 4;
    }
}
