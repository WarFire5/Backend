using Backend.Core.DTOs;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;

namespace Backend.Business.Services;

public interface IUsersService
{
    Guid AddUser(CreateUserRequest request);
    List<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
    //Guid CreateUser(string userName, string password, string email, int age);
    void UpdateUser(UpdateUserRequest request);
    void DeleteUserById(Guid id);
    public AuthenticatedResponse Login(LoginUserRequest request);
}