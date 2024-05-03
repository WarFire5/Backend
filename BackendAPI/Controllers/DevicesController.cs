using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Devices.Requests;
using BackendAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers;

//[Authorize]
[ApiController]
[Route("/api/devices")]

public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    ////метод для отображения выпадающего списка в свагере
    //[HttpPost()]
    //public ActionResult<Guid> AddDevice(Guid ownerId, DeviceType deviceType, [FromBody] AddDeviceRequest request)
    //{
    //    _logger.Information($"Девайс {request.DeviceName} типа {request.DeviceType} пользователя {ownerId}");

    //    return Ok(_devicesService.AddDevice(ownerId, deviceType, request));
    //}

    [HttpPost("{ownerId}")]
    public ActionResult<Guid> AddDevice(Guid ownerId, [FromBody] AddDeviceRequest request)
    {
        _logger.Information($"Девайс {request.DeviceName} типа {request.DeviceType} пользователя {ownerId}");

        return Ok(_devicesService.AddDevice(ownerId, request));
    }

    [HttpGet()]
    public ActionResult<List<DeviceDto>> GetDevices([FromQuery] Guid? id, [FromQuery] Guid? ownerId)
    {
        if (id is not null)
        {
            return Ok(_devicesService.GetDeviceById((Guid)id));
        }
        if (ownerId is not null)
        {
            return Ok(_devicesService.GetDeviceByOwnerId((Guid)ownerId));
        }

        return Ok(new List<DeviceDto>());
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound($"Девайс с Id {id} не найден");

        return Ok(_devicesService.GetDeviceById(id));
    }

    [HttpGet("by-owner/{ownerId}")]
    public DeviceDto GetDeviceByOwnerId(Guid ownerId)
    {
        return _devicesService.GetDeviceByOwnerId(ownerId);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _devicesService.DeleteDeviceById(id);
        return Ok();
    }
}
