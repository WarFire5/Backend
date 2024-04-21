using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MainerWomanContext context) : base(context)
    {
    }

    public List<UserDto> GetUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id) => _ctx.Users.SingleOrDefault(u => u.Id == id);
}
