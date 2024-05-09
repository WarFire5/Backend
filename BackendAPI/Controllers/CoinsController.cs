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
    public ActionResult GenerateCoinWithDevice([FromRoute] Guid deviceId, GenerateCoinsWithDeviceRequest request)
    {
        _logger.Information($"Добываем коины типа {request.CoinType} девайсом c Id {deviceId}.");
        return Ok(_devicesService.GenerateCoinsWithDevice(deviceId, request));
    }

    [HttpGet()]
    public ActionResult<List<OperationWithCoinsDto>> GetOperationsWithCoins()
    {
        _logger.Information($"Получаем список всех операций с коинами.");
        _coinsService.GetOperationsWithCoins();

        return Ok(new List<OperationWithCoinsDto>());
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
}
