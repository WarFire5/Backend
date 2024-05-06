using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/coins")]

public class CoinsController : Controller
{
    private readonly ICoinsService _coinsService;
    private readonly IDevicesService _devicesService;

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

    //[HttpPost]
    //public ActionResult GenerateCoinWithDevice(GenerateCoinWithDeviceRequest request)
    //{
    //    return Ok(_coinsService.GenerateCoinWithDevice(request));
    //}
}
