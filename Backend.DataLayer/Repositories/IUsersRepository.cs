using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IUsersRepository
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetUsers();
    public void DeleteUserById(UserDto user);
    public Guid CreateUser(UserDto user);

}