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

    [HttpGet("author2")]
    public string GetAuthor2()
    {
        return _author;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        _usersService.GetUsers();
        return Ok(new List<UserResponse>());
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<UserWithDevicesResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем юзера по айди {id}");
        _usersService.GetUserById(Guid.NewGuid());
        return Ok(new UserWithDevicesResponse());
    }

    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information($"{request.UserName} {request.Password}");
        //var id = _usersService.AddUser(new()
        //{
        //    UserName = request.UserName,
        //    Password = request.Password,
        //    Email = request.Email,
        //    Age = request.Age,
        //});

        return Ok(_usersService.AddUser(request));
    }

    //[HttpPost]
    //public ActionResult<Guid> CreateUser(string userName, string password, string email, int age)
    //{
    //    if (userName != null && password != null && email != null && age > 18 && age < 150)
    //    {
    //        return Ok(_usersService.CreateUser(userName, password, email, age));
    //    }

    //    return BadRequest();
    //}

    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest user)
    {
        if (user is null)
        {
            return BadRequest("Invalid client request");
        }

        return Ok(_usersService.Login(user));
    }

    [HttpPut]
    public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        _logger.Information($"{request.Id} {request.UserName}");
        _usersService.UpdateUser(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.DeleteUserById(id);
        return Ok();
    }

    // api/users/42/devices
    [HttpGet("{ownerId}/devices")]
    public DeviceDto GetDeviceByOwnerId(Guid ownerId)
    {
        return _devicesService.GetDeviceByOwnerId(ownerId);
    }

    // api/users/42/coins
    [HttpGet("{ownerId}/coins")]
    public CoinDto GetCoinByOwnerId(Guid ownerId)
    {
        return _coinsService.GetCoinByOwnerId(ownerId);
    }
}
