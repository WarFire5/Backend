using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/coins")]

public class CoinsController : Controller
{
    private readonly IDevicesService _devicesService;
    private readonly ICoinsService _coinsService;
    private readonly Serilog.ILogger _logger = Log.ForContext<CoinsController>();

    public CoinsController(ICoinsService coinsService)
    {
        _coinsService = coinsService;
    }

    [HttpGet()]
    public ActionResult<List<OperationWithCoinsDto>> GetCoins([FromQuery] Guid? id, [FromQuery] Guid? ownerId)
    {


        return Ok(new List<OperationWithCoinsDto>());
    }

    //[HttpGet()]
    //public DeviceDto GetCoinTypeByDeviceType (GetCoinTypeByDeviceTypeRequest request)
    //{
    //    return _devicesService.GetCoinTypeByDeviceType (request);
    //}

    [HttpPost("by-device/{deviceId}")]
    public ActionResult GenerateCoinWithDevice(GenerateCoinWithDeviceRequest request)
    {
        _logger.Information($"Добываем коины типа {request.CoinType} девайсом c Id {request.DeviceId}.");
        return Ok(_devicesService.GenerateCoinWithDevice(request));
    }
}
