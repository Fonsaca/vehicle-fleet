using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using VF.Api.Models;
using VF.Application.Features.Vehicles.Applications;
using VF.Application.Features.Vehicles.Dtos;
using VF.Domain.DataTypes;
using VF.Domain.Exceptions;
using VF.Domain.Models;

namespace VF.Api.Features.Vehicle
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly ILogger<VehicleController> _logger;

        private readonly IVehicleApplication _application;

        public VehicleController(ILogger<VehicleController> logger, IVehicleApplication application)
        {
            _logger = logger;
            _application = application;
        }


        [HttpGet(Name = "GetVehicles")]
        public async Task<ActionResult<ResponseApi<VehicleDto>>> GetVehicles(CancellationToken cancellationToken)
        {
            ResponseApi<VehicleDto> response;
            try
            {
                var vehicles = await _application.GetVehiclesAsync(cancellationToken);
                response = new ResponseApi<VehicleDto>(HttpStatusCode.OK, "Success", vehicles);
            }
            catch (Exception ex)
            {
                string message = $"An error occurred to get the vehicles: {ex.Message}";
                _logger.LogError(ex, message);
                response = new ResponseApi<VehicleDto>(HttpStatusCode.InternalServerError, message);
            }

            return StatusCode((int) response.StatusCode, response);
        }

        [HttpGet("serie/{chassisSerie}/number/{chassisNumber}", Name = "FindVehicle")]
        public async Task<ActionResult<ResponseApi<VehicleDto>>> FindVehicle(string chassisSerie, UInt32 chassisNumber, CancellationToken cancellationToken)
        {
            ResponseApi<VehicleDto> response;
            try
            {
                var id = new ChassisId()
                {
                    Serie = chassisSerie,
                    Number = chassisNumber
                };
                var vehicle = await _application.FindAsync(id,cancellationToken);

                if(vehicle == default)
                    response = new ResponseApi<VehicleDto>(HttpStatusCode.OK, "Vehicle not found");
                else
                    response = new ResponseApi<VehicleDto>(HttpStatusCode.OK, "Success", [vehicle]);
            }
            catch (Exception ex)
            {
                string message = $"An error occurred to get the vehicles: {ex.Message}";
                _logger.LogError(ex, message);
                response = new ResponseApi<VehicleDto>(HttpStatusCode.InternalServerError, message);
            }

            return StatusCode((int)response.StatusCode, response);
        }


        [HttpPost(Name = "CreateVehicle")]
        public async Task<ActionResult<ResponseApi<object>>> CreateVehicle(VehicleCreateDto createDto, CancellationToken cancellationToken)
        {
            ResponseApi<object> response;
            try
            {
                await _application.CreateAsync(createDto, cancellationToken);
                response = new ResponseApi<object>(HttpStatusCode.Created, "Success");
            }
            catch (VehicleValidationException ex)
            {
                string message = $"Validation error: {ex.ToString()}";
                _logger.LogError(ex, message);
                response = new ResponseApi<object>(HttpStatusCode.InternalServerError, message);
            }
            catch (Exception ex)
            {
                string message = $"An error occurred to create the vehicle: {ex.Message}";
                _logger.LogError(ex, message);
                response = new ResponseApi<object>(HttpStatusCode.InternalServerError, message);
            }

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut(Name = "EditVehicle")]
        public async Task<ActionResult<ResponseApi<object>>> EditVehicle(VehicleEditDto editDto, CancellationToken cancellationToken)
        {
            ResponseApi<object> response;
            try
            {
                await _application.EditAsync(editDto, cancellationToken);
                response = new ResponseApi<object>(HttpStatusCode.OK, "Success");
            }
            catch (KeyNotFoundException ex)
            {
                string message = $"Vehicle not found: {ex.ToString()}";
                _logger.LogError(ex, message);
                response = new ResponseApi<object>(HttpStatusCode.BadRequest, message);
            }
            catch (VehicleValidationException ex)
            {
                string message = $"Validation error: {ex.ToString()}";
                _logger.LogError(ex, message);
                response = new ResponseApi<object>(HttpStatusCode.BadRequest, message);
            }
            catch (Exception ex)
            {
                string message = $"An error occurred to edit the vehicle: {ex.Message}";
                _logger.LogError(ex, message);
                response = new ResponseApi<object>(HttpStatusCode.InternalServerError, message);
            }

            return StatusCode((int)response.StatusCode, response);
        }


    }
}
