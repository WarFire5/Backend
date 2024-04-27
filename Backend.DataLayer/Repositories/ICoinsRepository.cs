using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface ICoinsRepository
{
    CoinDto GetCoinById(Guid id);
    CoinDto GetCoinByOwnerId(Guid ownerId);
}