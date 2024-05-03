using Backend.Business.Services;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/devices")]

public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    [HttpPost]
    public ActionResult<Guid> AddDevice(string deviceName, string address, Guid ownerId)
    {

        if (deviceName != null && address != null)
        {
            return Ok(_devicesService.AddDevice(deviceName, address, ownerId));
        }

        return BadRequest();
    }

    [HttpGet()]
    public ActionResult<List<DeviceDto>> GetDevice([FromQuery] Guid? id, [FromQuery] Guid? ownerId)
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
