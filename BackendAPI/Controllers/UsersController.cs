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
    private readonly ICoinsService _coinsService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public UsersController(IUsersService usersService, IDevicesService devicesService, ICoinsService coinsService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
        _coinsService = coinsService;
    }

    [HttpGet("author")]
    public string GetAuthor()
    {
        return _author;
    }

    [HttpPost]
    public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
    {
        _logger.Information($"{request.Login} {request.Password}");

        return Ok(_usersService.AddUser(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest user)
    {
        if (user is null)
        {
            return BadRequest("Invalid client request");
        }

        return Ok(_usersService.Login(user));
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<UserWithDevicesResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем юзера по айди {id}");
        _usersService.GetUserById(Guid.NewGuid());

        return Ok(new UserWithDevicesResponse());
    }

    [Authorize]
    [HttpGet("{login}")]
    public ActionResult<UserWithDevicesResponse> GetUserByLogin(string login)
    {
        _logger.Information($"Получаем юзера по логину {login}");
        _usersService.GetUserByLogin(login);

        return Ok(new UserWithDevicesResponse());
    }

    [Authorize]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        _usersService.GetUsers();

        return Ok(new List<UserResponse>());
    }

    [Authorize]
    [HttpPut]
    public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        _logger.Information($"{request.Id} {request.Login}");
        _usersService.UpdateUser(request);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.DeleteUserById(id);

        return Ok();
    }

    [Authorize]
    [HttpGet("{ownerId}/devices")]
    public DeviceDto GetDeviceByOwnerId(Guid ownerId)
    {
        return _devicesService.GetDeviceByOwnerId(ownerId);
    }

    //[Authorize]
    //[HttpGet("{ownerId}/coins")]
    //public CoinDto GetQuantityCoinByOwnerId(Guid ownerId)
    //{
    //    return _coinsService.GetQuantityCoinByOwnerId(ownerId);
    //}
}