using Backend.Core.DTOs;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;

namespace Backend.Business.Services;

public interface IUsersService
{
    Guid AddUser(AddUserRequest request);
    public AuthenticatedResponse Login(LoginUserRequest request);
    UserDto GetUserById(Guid id);
    public UserDto GetUserByLogin(string login);
    void UpdateUser(UpdateUserRequest request);
    void DeleteUserById(Guid id);
    List<UserDto> GetUsers();
}