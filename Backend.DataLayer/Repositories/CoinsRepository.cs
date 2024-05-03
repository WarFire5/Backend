using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public class CoinsRepository : BaseRepository, ICoinsRepository
{
    public CoinsRepository(MainerWomanContext context) : base(context)
    {
    }
}
