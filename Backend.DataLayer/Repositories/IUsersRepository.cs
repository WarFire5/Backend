using Backend.Core.DTOs;

namespace Backend.DataLayer.Repositories;

public interface IUsersRepository
{
    Guid AddUser(UserDto user);
    UserDto GetUserById(Guid id);
    UserDto GetUserByUserName(string userName);
    List<UserDto> GetUsers();
    void UpdateUser(UserDto user);
    void DeleteUserById(UserDto user);
}