using Backend.Core.DTOs;
using Serilog;

namespace Backend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public UsersRepository(MainerWomanContext context) : base(context)
    {
    }

    public List<UserDto> GetUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information("Идём в базу данных искать юзера с Id {id}", id);
        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }

    public Guid CreateUser(UserDto user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();

        return user.Id;
    }

    public void DeleteUserById(UserDto user)
    {
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }
}
