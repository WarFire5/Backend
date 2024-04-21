using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface IUsersService
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetUsers();
    void DeleteUserById(Guid id);
}