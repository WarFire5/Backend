using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
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
    //public ActionResult<Guid> AddDevice(Guid ownerId, DeviceType deviceType, string deviceName)
    //{
    //    _logger.Information($"Девайс {deviceName} типа {deviceType} пользователя {ownerId}");

    //    return Ok(_devicesService.AddDevice(ownerId, deviceType, deviceName));
    //}

    [HttpPost("{ownerId}")]
    public ActionResult<Guid> AddDevice(Guid ownerId, [FromBody] AddDeviceRequest request)
    {
        _logger.Information($"Девайс {request.DeviceName} типа {request.DeviceType} для пользователя {ownerId} добавлен.");
        return Ok(_devicesService.AddDevice(ownerId, request));
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetDeviceById(Guid id)
    {
        if (id == Guid.Empty)
        {
            _logger.Information($"Девайс с Id {id} не найден.");
            return NotFound($"Девайс с Id {id} не найден.");
        }

        _logger.Information($"Получаем девайс с Id {id}");
        return Ok(_devicesService.GetDeviceById(id));
    }

    [HttpGet("by-owner/{ownerId}")]
    public ActionResult<List<DeviceDto>> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список девайсов для пользователя {ownerId}.");
        return Ok(_devicesService.GetDevicesByOwnerId(ownerId));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _logger.Information($"Удаляем девайс по Id {id}.");
        _devicesService.DeleteDeviceById(id);

        return NoContent();
    }

    [HttpPost("{deviceId}/coins")]
    public ActionResult GenerateCoinWithDevice(GenerateCoinWithDeviceRequest request)
    {
        _logger.Information($"Добываем коины типа {request.CoinType} девайсом c Id {request.DeviceId}.");
        return Ok(_devicesService.GenerateCoinWithDevice(request));
    }
}
