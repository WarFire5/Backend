using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BackendAPI.Controllers;

[ApiController]
[Route("/api/users")]

public class UsersController : Controller
{
    private const string _author = "Tanushka";

    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public UsersController(IUsersService usersService, IDevicesService devicesService, ICoinsService coinsService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
    }

    [HttpGet("author")]
    public string GetAuthor()
    {
        _logger.Information($"Получено имя автора.");
        return _author;
    }

    [HttpPost]
    public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
    {
        _logger.Information($"Пользователь с {request.Login} добавлен.");
        return Ok(_usersService.AddUser(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest user)
    {
        if (user is null)
        {
            _logger.Information($"Аутентификация не удалась.");
            return BadRequest("Invalid client request");
        }

        _logger.Information($"Аутентификация прошла успешно.");
        return Ok(_usersService.Login(user));
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<UserWithDevicesResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем пользователя по айди {id}.");
        _usersService.GetUserById(Guid.NewGuid());

        _logger.Information($"Возвращаем список девайсов пользователя с айди {id}.");
        return Ok(new UserWithDevicesResponse());
    }

    [Authorize]
    [HttpGet("{login}")]
    public ActionResult<UserWithDevicesResponse> GetUserByLogin(string login)
    {
        _logger.Information($"Получаем юзера по логину {login}.");
        _usersService.GetUserByLogin(login);

        _logger.Information($"Возвращаем список девайсов пользователя с логином {login}.");
        return Ok(new UserWithDevicesResponse());
    }

    [Authorize]
    [HttpPut]
    public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        _logger.Information($"Обновили данные пользователя с {request.Id}.");
        _usersService.UpdateUser(request);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _logger.Information($"Удаляем пользователя по Id {id}.");
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    [Authorize]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        _logger.Information($"Получаем список дпользователей.");
        _usersService.GetUsers();

        return Ok(new List<UserResponse>());
    }

    [Authorize]
    [HttpGet("{ownerId}/devices")]
    public ActionResult<List<DeviceDto>> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список девайсов для пользователя с айди {ownerId}.");
        return Ok(_devicesService.GetDevicesByOwnerId(ownerId));
    }

    //[Authorize]
    //[HttpGet("{ownerId}/coins")]
    //public CoinDto GetQuantityCoinsByOwnerId(Guid ownerId)
    //{
    //    return _coinsService.GetQuantityCoinsByOwnerId(ownerId);
    //}
}