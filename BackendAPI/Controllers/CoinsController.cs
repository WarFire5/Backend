using Backend.Business.Services;
using Backend.Core.Enums;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers;

//[Authorize]
[ApiController]
[Route("/api/coins")]

public class CoinsController : Controller
{
    private readonly IDevicesService _devicesService;
    private readonly ICoinsService _coinsService;
    private readonly Serilog.ILogger _logger = Log.ForContext<CoinsController>();

    public CoinsController(IDevicesService devicesService, ICoinsService coinsService)
    {
        _devicesService = devicesService;
        _coinsService = coinsService;
    }

    // генерируем коины с помощью девайса с указанным айди
    [HttpPost("coins-by-{deviceId}")]
    public ActionResult GenerateCoinWithDevice(Guid deviceId, CoinTypeAndQuantityRequest request)
    {
        _logger.Information($"Генерируем коины типа {request.CoinType} девайсом c Id {deviceId}.");
        return Ok(_devicesService.GenerateCoinsWithDevice(deviceId, request));
    }

    // получаем список типов коинов по выбранному типу девайса
    [HttpGet("coin-types-by-{deviceType}")]
    public ActionResult<List<CoinTypeResponse>> GetCoinTypesByDeviceType(DeviceType deviceType)
    {
        _logger.Information($"Получаем список типов коинов по типу девайса {deviceType}.");
        return Ok(_coinsService.GetCoinTypesByDeviceType(deviceType));
    }

    // получаем список всех операций с коинами
    [HttpGet("list-operation-with-coins")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationsWithCoins()
    {
        _logger.Information($"Получаем список всех операций с коинами.");
        return Ok(_coinsService.GetOperationsWithCoins());
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

    // получаем список типов коинов с их количеством по овнер айди
    [HttpGet("list-coin-types-with-quantity-by-owner-id/{ownerId}")]
    public ActionResult<List<CoinTypeAndQuantityResponse>> GetListCoinTypesWithQuantityByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список типов коинов с их количеством для пользователя c Id {ownerId}.");
        return Ok(_coinsService.GetListCoinTypesWithQuantityByOwnerId(ownerId));
    }
}