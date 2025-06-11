using AutoMapper;
using VF.Application.Features.Vehicles.Dtos;
using VF.Domain.Models;

namespace VF.Application.Features.Vehicles.Converters
{
    public class VehicleDtoProfile : Profile
    {
        public VehicleDtoProfile()
        {

            CreateMap<Vehicle, VehicleDto>();


            CreateMap<VehicleCreateDto, Vehicle>()
                .ConstructUsing((dto, ctx) =>
                {
                    return dto.Type switch
                    {
                        "Bus" => new Bus(),
                        "Truck" => new Truck(),
                        "Car" => new Car(),
                        _ => throw new NotImplementedException("Vehicle type is unknown")
                    };
                });


        }
    }
}
