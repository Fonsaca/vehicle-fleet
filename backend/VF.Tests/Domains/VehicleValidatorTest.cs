using FluentValidation;
using Moq;
using VF.Domain.DataTypes;
using VF.Domain.Features.Vehicles.Repositories;
using VF.Domain.Features.Vehicles.Validators;
using VF.Domain.Models;

namespace VF.Tests.Domains
{
    public class VehicleValidatorTest
    {
        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldFailWhenChassisIdIsTheDefaultValue(string type)
        {
            Task<Vehicle?> nullVehicleTask = Task.Run(() => default(Vehicle));

            var vehicle = GetVehicle(type);
            vehicle.Color = "Red";

            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(nullVehicleTask);

            var validator = new VehicleMgmtValidator(mockRepository.Object);

            var result = await validator.ValidateAsync(vehicle);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(Vehicle.ChassisId), result.Errors[0].PropertyName);
            Assert.Equal("Chassis Id must be informed", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldFailWhenSameVehicleAlreadyExists(string type)
        {
            Task<Vehicle?> vehicleTask = Task.Run(() => GetVehicle(type));

            var vehicle = GetVehicle(type);
            vehicle.ChassisId = new ChassisId() { Number = 1, Serie = "0000001" };
            vehicle.Color = "Red";

            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(vehicleTask);

            var validator = new VehicleMgmtValidator(mockRepository.Object);

            var result = await validator.ValidateAsync(vehicle);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(Vehicle.ChassisId), result.Errors[0].PropertyName);
            Assert.Equal("Chassis Id must be unique", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldFailWhenColorIsEmpty(string type)
        {
            Task<Vehicle?> nullVehicleTask = Task.Run(() => default(Vehicle));

            var vehicle = GetVehicle(type);
            vehicle.ChassisId = new ChassisId() { Number = 1, Serie = "0000001" };
            vehicle.Color = string.Empty;

            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(nullVehicleTask);

            var validator = new VehicleMgmtValidator(mockRepository.Object);

            var result = await validator.ValidateAsync(vehicle);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(Vehicle.Color), result.Errors[0].PropertyName);
            Assert.Equal("Color must be informed", result.Errors[0].ErrorMessage);
        }


        [Theory]
        [InlineData("Bus")]
        [InlineData("Truck")]
        [InlineData("Car")]
        public async Task ShouldBeValid(string type)
        {
            Task<Vehicle?> nullVehicleTask = Task.Run(() => default(Vehicle));

            var vehicle = GetVehicle(type);
            vehicle.ChassisId = new ChassisId() { Number = 1, Serie = "0000001" };
            vehicle.Color = "Red";

            var mockRepository = new Mock<IVehicleRepository>();
            mockRepository
                .Setup(c => c.FindAsync(It.IsAny<ChassisId>(), It.IsAny<CancellationToken>()))
                .Returns(nullVehicleTask);

            var validator = new VehicleMgmtValidator(mockRepository.Object);

            var result = await validator.ValidateAsync(vehicle);

            Assert.True(result.IsValid);
        }



        private Vehicle GetVehicle(string type)
        {
            return type switch
            {
                "Bus" => new Bus(),
                "Truck" => new Bus(),
                "Car" => new Bus(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}