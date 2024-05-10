using Backend.Business.Services;
using Backend.Core.DTOs;
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

    //[HttpGet()]
    //public DeviceDto GetCoinTypeByDeviceType (GetCoinTypeByDeviceTypeRequest request)
    //{
    //    return _devicesService.GetCoinTypeByDeviceType (request);
    //}

    [HttpPost("by-device/{deviceId}")]
    public ActionResult GenerateCoinWithDevice([FromRoute] Guid deviceId, CoinsWithDeviceRequest request)
    {
        _logger.Information($"Добываем коины типа {request.CoinType} девайсом c Id {deviceId}.");
        return Ok(_devicesService.GenerateCoinsWithDevice(deviceId, request));
    }

    [HttpGet()]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationsWithCoins()
    {
        _logger.Information($"Получаем список всех операций с коинами.");
        var result = _coinsService.GetOperationsWithCoins();

        return Ok(result);
    }

    [HttpGet("by-device/{deviceId}")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationWithCoinsByDeviceId(Guid deviceId)
    {
        _logger.Information($"Получаем список всех операций с коинами для девайса с айди {deviceId}.");
        return Ok(_coinsService.GetOperationWithCoinsByDeviceId(deviceId));
    }

    [HttpGet("by-device/{deviceId},{coinType}")]
    public ActionResult<List<OperationWithCoinsResponse>> GetOperationWithCoinsByDeviceIdFromCoinType(Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем список операций с коинами типа {coinType} для девайса с айди {deviceId}.");
        return Ok(_coinsService.GetOperationWithCoinsByDeviceIdFromCoinType(deviceId, coinType));
    }

    [HttpGet("by-device/quantity-for-type")]
    public ActionResult<CoinQuantityForCoinTypeResponse> GetCoinQuantityFromCurrentTypeForCurrentDeviceId(Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем количество коинов типа {coinType} для девайса с айди {deviceId}.");

        return Ok(_coinsService.GetCoinQuantityFromCurrentTypeForCurrentDeviceId(deviceId, coinType));
    }

    [HttpGet("by-device/quantity-for-device")]
    public ActionResult<List<CoinTypesWithQuantityResponse>> GetListCoinTypesWithQuantityByDeviceId(Guid deviceId)
    {
        _logger.Information($"Получаем список типов коинов с их количеством для девайса с айди {deviceId}.");

        return Ok(_coinsService.GetListCoinTypesWithQuantityByDeviceId(deviceId));
    }

    [HttpGet("by-device/quantity-for-owner")]
    public ActionResult<ListCoinTypesWithQuantityResponse> GetListCoinTypesWithQuantityByOwnerId([FromRoute] Guid ownerId, Guid deviceId, CoinType coinType)
    {
        _logger.Information($"Получаем список типов коинов с их количеством для пользователя с айди {ownerId}.");

        return Ok(_coinsService.GetListCoinTypesWithQuantityByOwnerId(ownerId, deviceId, coinType));
    }
}
