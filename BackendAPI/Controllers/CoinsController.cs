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

    // api/coins
    [HttpGet()]
    public ActionResult<List<CoinDto>> GetCoin([FromQuery] Guid? ownerId, [FromQuery] Guid? id)
    {
        if (ownerId is not null)
        {
            return Ok(_coinsService.GetCoinByOwnerId((Guid)ownerId));
        }
        if (id is not null)
        {
            return Ok(_coinsService.GetCoinById((Guid)id));
        }

        return Ok(new List<CoinDto>());
    }

    // api/coins/42
    [HttpGet("{id}")]
    public ActionResult<CoinDto> GetCoinById(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound($"Девайс с Id {id} не найден");

        return Ok(_coinsService.GetCoinById(id));
    }

    // api/coins/by-owner/42
    [HttpGet("by-owner/{ownerId}")]
    public CoinDto GetCoinByOwnerId(Guid ownerId)
    {
        return _coinsService.GetCoinByOwnerId(ownerId);
    }
}
