using Backend.Business.Services;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("/api/devices")]

public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    // api/devices
    [HttpGet()]
    public ActionResult <List<DeviceDto>> GetDevice([FromQuery] Guid? ownerId, [FromQuery] Guid? id)
    {
        if (ownerId is not null)
        {
            return Ok(_devicesService.GetDeviceByOwnerId((Guid)ownerId));
        }
        if (id is not null)
        {
            return Ok(_devicesService.GetDeviceById((Guid)id));
        }

        return Ok(new List<DeviceDto>());
    }

    // api/devices/42
    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound($"Девайс с Id {id} не найден");

        return Ok(_devicesService.GetDeviceById(id));
    }

    // api/devices/by-owner/42
    [HttpGet("by-owner/{ownerId}")]
    public DeviceDto GetDeviceByOwnerId(Guid ownerId)
    {
        return _devicesService.GetDeviceByOwnerId(ownerId);
    }
}
