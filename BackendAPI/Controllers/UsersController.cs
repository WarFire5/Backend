using Backend.Business.Services;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;

[ApiController]
[Route("/api/users")]

public class UsersController : Controller
{
    private const string _author = "Tanushka";
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
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

    [HttpGet]
    public List<UserDto> GetUsers()
    {
        return _usersService.GetUsers();
    }

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id)
    {
        return _usersService.GetUserById(Guid.NewGuid());
    }

    [HttpPost]
    public ActionResult<Guid> CreateUser(string userName, string password, string email, int age)
    {
        if (userName != null && password != null && email != null && age > 0)
        {
            return Ok(_usersService.CreateUser(userName, password, email, age));
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute] Guid id, [FromBody] object request)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        //try
        //{
        //    _usersService.DeleteUserById(id);
        //}
        //catch (Exception ex)
        //{
        //    return NotFound(ex.Message);
        //}

        _usersService.DeleteUserById(id);
        return Ok();
    }

    // api/users/42/devices
    [HttpGet("{ownerId}/devices")]
    public DeviceDto GetDeviceByOwnerId(Guid ownerId)
    {
        return _devicesService.GetDeviceByOwnerId(ownerId);
    }
}
