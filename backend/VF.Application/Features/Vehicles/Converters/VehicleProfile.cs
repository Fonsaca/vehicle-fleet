using AutoMapper;
using VF.Application.Features.Vehicles.Dtos;
using VF.Domain.Models;

namespace VF.Application.Features.Vehicles.Converters
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {

            CreateMap<Vehicle, VehicleDto>();

           

        }
    }
}
