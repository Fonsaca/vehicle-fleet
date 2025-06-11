using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Database.Context.DbEntities;
using VF.Domain.DataTypes;
using VF.Domain.Models;

namespace VF.Database.Features.Vehicles.Converters
{
    public class VehicleEntityProfile : Profile
    {
        public VehicleEntityProfile()
        {
            CreateMap<Vehicle, VehicleEntity>()
                .ForMember(m => m.ChassisNumber, opt => opt.MapFrom(x => x.ChassisId.Number))
                .ForMember(m => m.ChassisSerie, opt => opt.MapFrom(x => x.ChassisId.Serie));


            CreateMap<VehicleEntity, Vehicle>()
                .ConstructUsing((dto, ctx) =>
                {
                    return dto.Type switch
                    {
                        "Bus" => new Bus(),
                        "Truck" => new Truck(),
                        "Car" => new Car(),
                        _ => throw new NotImplementedException("Vehicle type is unknown")
                    };
                })
                .ForMember(m => m.ChassisId, opt => opt.MapFrom(x => new ChassisId() { Serie = x.ChassisSerie, Number = x.ChassisNumber }));

        }
    }
}
