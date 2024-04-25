using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface IUsersService
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetUsers();
    Guid CreateUser(string userName, string password, string email, int age);
    void DeleteUserById(Guid id);
    Guid AddUser(UserDto user);
}