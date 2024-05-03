using Backend.Business.Services;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/coins")]

public class CoinsController : Controller
{
    private readonly ICoinsService _coinsService;

    public CoinsController(ICoinsService coinsService)
    {
        _coinsService = coinsService;
    }

    [HttpGet()]
    public ActionResult<List<CoinDto>> GetCoins([FromQuery] Guid? id, [FromQuery] Guid? ownerId)
    {


        return Ok(new List<CoinDto>());
    }
}
