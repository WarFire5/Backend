using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Requests;
using Backend.Core.Models.Devices.Responses;
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
    private readonly ICoinsService _coinsService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public DevicesController(IDevicesService devicesService, ICoinsService coinsService)
    {
        _devicesService = devicesService;
        _coinsService = coinsService;
    }

    [HttpPost("{ownerId}")]
    public ActionResult<Guid> AddDevice(Guid ownerId, [FromBody] DeviceRequest request)
    {
        _logger.Information($"Девайс {request.Name} типа {request.Type} для пользователя {ownerId} добавлен.");
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

    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _logger.Information($"Удаляем девайс по Id {id}.");
        _devicesService.DeleteDeviceById(id);

        return NoContent();
    }

    [HttpGet]
    public ActionResult<List<DeviceResponse>> GetDevices()
    {
        _logger.Information($"Получаем список девайсов.");
        var result = _devicesService.GetDevices();

        return Ok(result);
    }

    [HttpGet("by-owner/{ownerId}")]
    public ActionResult<List<DeviceDto>> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список девайсов для пользователя с айди {ownerId}.");
        return Ok(_devicesService.GetDevicesByOwnerId(ownerId));
    }

    [HttpPost("{deviceId}/coins")]
    public ActionResult GenerateCoinWithDevice([FromRoute] Guid deviceId, CoinTypeAndQuantityRequest request)
    {
        _logger.Information($"Добываем коины типа {request.Type} девайсом c Id {deviceId}.");
        return Ok(_devicesService.GenerateCoinsWithDevice(deviceId, request));
    }

    [HttpGet("{deviceId}/coins")]
    public ActionResult<List<OperationWithCoinsDto>> GetOperationWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Получаем список операций с коинами для девайса с айди {deviceId}.");
        return Ok(_coinsService.GetOperationWithCoinsByDeviceId(deviceId));
    }

    [HttpGet("{deviceId},{coinType}/coins")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем список операций с коинами типа {coinType} для девайса с айди {deviceId}.");
        return Ok(_coinsService.GetOperationWithCoinsByDeviceIdFromCoinType(deviceId, coinType));
    }
}
