using Backend.Core.DTOs;
using Backend.DataLayer.Repositories;

namespace Backend.Business.Services;

public class CoinsService : ICoinsService
{
    private readonly ICoinsRepository _coinsRepository;
    private readonly IUsersRepository _usersRepository;
    public CoinsService(ICoinsRepository coinsRepository, IUsersRepository usersRepository)
    {
        _coinsRepository = coinsRepository;
        _usersRepository = usersRepository;
    }
    public CoinDto GetCoinById(Guid id) => _coinsRepository.GetCoinById(id);
    public CoinDto GetCoinByOwnerId(Guid ownerId) => _coinsRepository.GetCoinByOwnerId(ownerId);
}
