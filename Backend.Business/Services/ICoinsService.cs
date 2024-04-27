using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface ICoinsService
{
    CoinDto GetCoinById(Guid id);
    CoinDto GetCoinByOwnerId(Guid ownerId);
}