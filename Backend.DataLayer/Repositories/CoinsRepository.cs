using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public class CoinsRepository : BaseRepository, ICoinsRepository
{
    public CoinsRepository(MainerWomanContext context) : base(context)
    {
    }

    public CoinDto GetCoinById(Guid id) => _ctx.Coins.FirstOrDefault(c => c.Id == id);
    public CoinDto GetCoinByOwnerId(Guid ownerId) => _ctx.Coins.FirstOrDefault(c => c.Owner.Id == ownerId);

}
