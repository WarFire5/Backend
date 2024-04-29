using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;
using Backend.DataLayer.Repositories;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Backend.Business.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private const string pepper = "5_555_5";
    private const int iteration = 5;

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
        user.PasswordSalt = PasswordHasher.GenerateSalt();
        user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, pepper, iteration);

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

    public AuthenticatedResponse Login(LoginUserRequest request)
    {
        _logger.Debug($"Ищем пользователя в базе по логину: {request.UserName}");
        var user = _usersRepository.GetUserByUserName(request.UserName);


        if (user == null) throw new NotFoundException("Username or password did not match.");

        if (CheckPassword(request, user))
        {
            _logger.Debug($"Выдаём пользователю {request.UserName} токен.");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Backend_Backend_Backend_superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "Backend",
                audience: "UI",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new AuthenticatedResponse { Token = tokenString };
        }

        _logger.Debug($"Пользователь {request.UserName} не найден.");
        throw new ValidationException("Username or password did not match.");
    }

    private bool CheckPassword(LoginUserRequest request, UserDto user)
    {
        var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, pepper, iteration);
        if (user.PasswordHash != passwordHash)
        {
            Log.Debug($"Пароль пользователя не совпадает: {request.UserName}");
            throw new AuthenticationException("Username or password did not match.");
        }

        return true;
    }

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
