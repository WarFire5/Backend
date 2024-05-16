using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Coins.Responses;
using Backend.Core.Models.Devices.Responses;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;
using Backend.DataLayer.Repositories;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.Business.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private readonly IValidator<AddUserRequest> _addUserValidator;

    private const string pepper = "5_555_5";
    private const int iteration = 5;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IValidator<AddUserRequest> addUserValidator)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _addUserValidator = addUserValidator;
    }

    public Guid AddUser(AddUserRequest request)
    {
        var validationResult = _addUserValidator.Validate(request);
        if (validationResult.IsValid)
        {
            var user = _mapper.Map<UserDto>(request);
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, pepper, iteration);

            return _usersRepository.AddUser(user);
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        throw new ValidationException(exceptions);
    }

    public AuthenticatedResponse Login(LoginUserRequest request)
    {
        _logger.Debug($"Ищем пользователя в базе по логину: {request.Login}");
        var user = _usersRepository.GetUserByLogin(request.Login);

        if (user == null) throw new NotFoundException("Login or password did not match.");

        if (CheckPassword(request, user))
        {
            _logger.Debug($"Выдаём пользователю с логином {request.Login} токен.");
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

        _logger.Debug($"Пользователь с логином {request.Login} не найден.");
        throw new ValidationException("Login or password did not match.");
    }

    private bool CheckPassword(LoginUserRequest request, UserDto user)
    {
        var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, pepper, iteration);
        if (user.PasswordHash != passwordHash)
        {
            Log.Debug($"Пароль пользователя {request.Login} не совпадает.");
            throw new AuthenticationException("Login or password did not match.");
        }

        return true;
    }

    public UserDto GetUserById(Guid id) => _usersRepository.GetUserById(id);

    public UserDto GetUserByLogin(string login) => _usersRepository.GetUserByLogin(login);

    public void UpdateUser(UpdateUserRequest request)
    {
        var user = _usersRepository.GetUserById(request.Id);
        if (user is null)
        {
            throw new NotFoundException($"Юзер с Id {request.Id} не найден");
        }
        user.Login = request.Login;
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
    
    public List<UserResponse> GetUsers()
    {
        var users = _usersRepository.GetUsers();
        var result =_mapper.Map<List<UserResponse>>(users);

        return result;
    }
}