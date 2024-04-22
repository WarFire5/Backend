using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IUsersRepository
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetUsers();
    Guid CreateUser(UserDto user);
    void DeleteUserById(UserDto user);
}