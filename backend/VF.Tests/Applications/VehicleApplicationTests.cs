using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Application.Features.Vehicles.Applications.Src;
using VF.Application.Features.Vehicles.Converters;
using VF.Application.Features.Vehicles.Dtos;
using VF.Database.Features.Vehicles.Converters;
using VF.Domain.DataTypes;
using VF.Domain.Exceptions;
using VF.Domain.Features.Vehicles.Repositories;
using VF.Domain.Features.Vehicles.Validators;
using VF.Domain.Models;

namespace VF.Tests.Applications
{
    public class VehicleApplicationTests
    {

        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldFailWhenTryingToCreateAnInvalidVehicle(string type)
        {

            //automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleDtoProfile>();
                cfg.AddProfile<VehicleEntityProfile>();
            });

            var _mapper = config.CreateMapper();

            //Repository
            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.CreateAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()));


            //application
            var application = new VehicleApplication(mockRepository.Object, _mapper);

            //Dto
            var dto = new VehicleCreateDto()
            {
                ChassisId = new ChassisId(),
                Color = "Blue",
                Type = type
            };

            await Assert.ThrowsAsync<VehicleValidationException>(async () => await application.CreateAsync(dto, CancellationToken.None));
        }

        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldCreateVehicle(string type)
        {

            //automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleDtoProfile>();
                cfg.AddProfile<VehicleEntityProfile>();
            });

            var _mapper = config.CreateMapper();

            //Repository
            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.CreateAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()));


            //application
            var application = new VehicleApplication(mockRepository.Object, _mapper);

            //Dto
            var dto = new VehicleCreateDto()
            {
                ChassisId = new ChassisId() {  Number = 1, Serie = "0001"},
                Color = "Blue",
                Type = type
            };

            await application.CreateAsync(dto, CancellationToken.None);

            Assert.True(true);
        }



        [Fact]
        public async Task ShouldFailWhenEditVehicleNotExists()
        {
            Task<Vehicle?> nullVehicleTask = Task.Run(() => default(Vehicle));

            //automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleDtoProfile>();
                cfg.AddProfile<VehicleEntityProfile>();
            });

            var _mapper = config.CreateMapper();

            //Repository
            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(nullVehicleTask);


            //application
            var application = new VehicleApplication(mockRepository.Object, _mapper);

            //Dto
            var dto = new VehicleEditDto()
            {
                ChassisId = new ChassisId() { Number = 1, Serie = "0001" },
                Color = "Blue",
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await application.EditAsync(dto, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldEditVehicle()
        {
            Task<Vehicle?> vehicleTask = Task.Run(() => (Vehicle?)new Bus());

            //automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleDtoProfile>();
                cfg.AddProfile<VehicleEntityProfile>();
            });

            var _mapper = config.CreateMapper();

            //Repository
            var mockRepository = new Mock<IVehicleRepository>();

            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(vehicleTask);

            mockRepository
                .Setup(c => c.CreateAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()));


            //application
            var application = new VehicleApplication(mockRepository.Object, _mapper);

            //Dto
            var dto = new VehicleEditDto()
            {
                ChassisId = new ChassisId() { Number = 1, Serie = "0001" },
                Color = "Red"
            };

            await application.EditAsync(dto, CancellationToken.None);

            Assert.True(true);
        }


    }
}
