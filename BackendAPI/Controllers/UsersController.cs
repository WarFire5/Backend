using Backend.Business.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Devices.Responses;
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

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
    }

    // получаем имя автора
    [HttpGet("author")]
    public string GetAuthor()
    {
        _logger.Information($"Получено имя автора.");
        return _author;
    }

    // добавляем пользователя
    [HttpPost("user")]
    public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
    {
        _logger.Information($"Пользователь с {request.Login} добавлен.");
        return Created("", _usersService.AddUser(request));
    }

    // авторизуем пользователя
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

    // получаем пользователя по айди
    //[Authorize]
    [HttpGet("user-by-{id}")]
    public ActionResult<UserDto> GetUserById(Guid id)
    {
        if (id == Guid.Empty)
        {
            _logger.Information($"Пользователь с Id {id} не найден.");
            return NotFound($"Пользователь с Id {id} не найден.");
        }

        _logger.Information($"Получаем пользователя с Id {id}");
        return Ok(_usersService.GetUserById(id));
    }

    // получаем пользователя по логину
    //[Authorize]
    [HttpGet("user-by-login/{login}")]
    public ActionResult<UserDto> GetUserByLogin(string login)
    {
        if (login == "")
        {
            _logger.Information($"Пользователь с логином {login} не найден.");
            return NotFound($"Пользователь с логином {login} не найден.");
        }

        _logger.Information($"Получаем пользователя с логином {login}");
        return Ok(_usersService.GetUserByLogin(login));
    }

    // обновляем данные пользователя по айди
    [Authorize]
    [HttpPut("user-by-{id}")]
    public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        _logger.Information($"Обновили данные пользователя с {request.Id}.");
        _usersService.UpdateUser(request);

        return Ok();
    }

    // удаляем пользователя по айди
    [Authorize]
    [HttpDelete("user-by-{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _logger.Information($"Удаляем пользователя по Id {id}.");
        _usersService.DeleteUserById(id);

        return NoContent();
    }

    // получаем список пользователей
    //[Authorize]
    [HttpGet("list-users")]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        _logger.Information($"Получаем список пользователей.");
        return Ok(_usersService.GetUsers());
    }

    // получаем список девайсов по овнер айди
    //[Authorize]
    [HttpGet("list-devices-by-{ownerId}")]
    public ActionResult<List<DeviceResponse>> GetDevicesByOwnerId(Guid ownerId)
    {
        _logger.Information($"Получаем список девайсов для пользователя с Id {ownerId}.");
        return Ok(_devicesService.GetDevicesByOwnerId(ownerId));
    }
}