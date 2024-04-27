using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users.Requests;
using Backend.DataLayer.Repositories;
using Serilog;

namespace Backend.Business.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public Guid AddUser(CreateUserRequest request)
    {
        //if (user.Age < 18 || user.Age > 150)
        //{
        //    throw new ValidationException("Возраст указан некорректно.");
        //}
        //if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
        //{
        //    throw new ValidationException("Что-то не так с паролем.");
        //}

        var user = _mapper.Map<UserDto>(request);
        return _usersRepository.CreateUser(user);
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

    //public Guid CreateUser(string userName, string password, string email, int age)
    //{
    //    UserDto user = new UserDto()
    //    {
    //        Id = Guid.NewGuid(),
    //        UserName = userName,
    //        Password = password,
    //        Email = email,
    //        Age = age,
    //        Devices = new List<DeviceDto>(),
    //        Coins = new List<CoinDto>()
    //    };

    //    return _usersRepository.CreateUser(user);
    //}

    public void UpdateUser(UpdateUserRequest request)
    {
        var user = _usersRepository.GetUserById(request.Id);
        if (user is null)
        {
            throw new NotFoundException($"Юзер с Id {request.Id} не найден");
        }
        user.UserName = request.UserName;
        user.Email = request.Email;
        user.Age = request.Age;

        _usersRepository.UpdateUser(user);
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
