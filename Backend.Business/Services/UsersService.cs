using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.DataLayer.Repositories;
using Serilog;

namespace Backend.Business.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Guid AddUser(UserDto user)
    {
        if (user.Age <18 || user.Age > 150)
        {
            throw new ValidationException("Возраст указан некорректно.");
        }
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
        {
            throw new ValidationException("Что-то не так с паролем.");
        }
        return Guid.NewGuid();
    }

    public List<UserDto> GetUsers()
    {
        // здесь есть бизнес логика
        return _usersRepository.GetUsers();
    }

    public UserDto GetUserById(Guid id)
    {
        // здесь есть бизнес логика
        return _usersRepository.GetUserById(id);
    }

    public Guid CreateUser(string userName, string password, string email, int age)
    {
        UserDto user = new UserDto()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Password = password,
            Email = email,
            Age = age,
            Devices = new List<DeviceDto>(),
            Coins = new List<CoinDto>()
        };

        return _usersRepository.CreateUser(user);
    }

    public void DeleteUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is null)
        {
            throw new NotFoundException($"Юзер с Id {id} не найден");
        }

        _usersRepository.DeleteUserById(user);
    }
}
