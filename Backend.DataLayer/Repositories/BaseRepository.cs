namespace Backend.DataLayer.Repositories;

public class BaseRepository
{
    protected readonly MainerWomanContext _ctx;
    public BaseRepository(MainerWomanContext context)
    {
        _ctx = context;
    }
}
