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
    public ActionResult<List<CoinDto>> GetCoin([FromQuery] Guid? id, [FromQuery] Guid? ownerId)
    {
        if (id is not null)
        {
            return Ok(_coinsService.GetCoinById((Guid)id));
        }
        if (ownerId is not null)
        {
            return Ok(_coinsService.GetCoinByOwnerId((Guid)ownerId));
        }

        return Ok(new List<CoinDto>());
    }

    [HttpGet("{id}")]
    public ActionResult<CoinDto> GetCoinById(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound($"Девайс с Id {id} не найден");

        return Ok(_coinsService.GetCoinById(id));
    }

    [HttpGet("by-owner/{ownerId}")]
    public CoinDto GetCoinByOwnerId(Guid ownerId)
    {
        return _coinsService.GetCoinByOwnerId(ownerId);
    }
}
