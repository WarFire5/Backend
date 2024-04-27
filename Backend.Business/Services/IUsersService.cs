using Backend.Core.DTOs;

namespace Backend.Business.Services;

public interface IUsersService
{
    //Guid AddUser(UserDto user);
    List<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
    Guid CreateUser(string userName, string password, string email, int age);
    void DeleteUserById(Guid id);
}