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

    // добавляем девайс пользователю по айди
    [HttpPost("device-for-{ownerId}")]
    public ActionResult<Guid> AddDevice(Guid ownerId, [FromBody] DeviceRequest request)
    {
        _logger.Information($"Девайс {request.DeviceName} типа {request.DeviceType} для пользователя {ownerId} добавлен.");
        return Ok(_devicesService.AddDevice(ownerId, request));
    }

    // получаем девайс по айди
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

    // удаляем девайс по айди
    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _logger.Information($"Удаляем девайс по Id {id}.");
        _devicesService.DeleteDeviceById(id);

        return NoContent();
    }

    // получаем все девайсы
    [HttpGet("list-devices")]
    public ActionResult<List<DeviceResponse>> GetDevices()
    {
        _logger.Information($"Получаем список девайсов.");
        var result = _devicesService.GetDevices();

        return Ok(result);
    }

    // получаем девайсы по овнер айди
    [HttpGet("list-devices-by-{ownerId}")]
    public ActionResult<List<DeviceResponse>> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список девайсов для пользователя с Id {ownerId}.");
        return Ok(_devicesService.GetDevicesByOwnerId(ownerId));
    }

    // генерируем коины с помощью девайса с указанным айди
    [HttpPost("coins-by-{deviceId}")]
    public ActionResult GenerateCoinWithDevice(Guid deviceId, CoinTypeAndQuantityRequest request)
    {
        _logger.Information($"Генерируем коины типа {request.CoinType} девайсом c Id {deviceId}.");
        return Ok(_devicesService.GenerateCoinsWithDevice(deviceId, request));
    }

    // получаем список всех операций с коинами по девайс айди
    [HttpGet("list-operation-with-coins-by-{deviceId}")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationsWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Получаем список всех операций с коинами для девайса c Id {deviceId}.");
        return Ok(_coinsService.GetOperationsWithCoinsByDeviceId(deviceId));
    }

    // получаем список всех операций с коинами выбранного типа по девайс айди
    [HttpGet("list-operation-with-coins-for-{coinType}-by-{deviceId}")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationWithCoinsForCoinTypeByDeviceId(Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем список всех операций с коинами типа {coinType} для девайса c Id {deviceId}.");
        return Ok(_coinsService.GetOperationsWithCoinsForCoinTypeByDeviceId(deviceId, coinType));
    }

    // получаем количество коинов выбранного типа по девайс айди
    [HttpGet("coin-quantity-for-{coinType}-by-{deviceId}")]
    public ActionResult<CoinTypeAndQuantityResponse> GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем количество коинов типа {coinType} для девайса c Id {deviceId}.");
        return Ok(_coinsService.GetCoinQuantityFromCurrentTypeForCurrentDeviceId(deviceId, coinType));
    }

    // получаем список типов коинов с их количеством по девайс айди
    [HttpGet("list-coin-types-with-quantity-by-device-id/{deviceId}")]
    public ActionResult<List<CoinTypeAndQuantityResponse>> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId)
    {
        _logger.Information($"Получаем список типов коинов с их количеством для девайса c Id {deviceId}.");
        return Ok(_coinsService.GetListCoinTypesWithQuantityByDeviceId(deviceId));
    }
}
