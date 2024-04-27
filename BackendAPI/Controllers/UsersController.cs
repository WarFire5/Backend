﻿using Backend.API.Models.Requests;
using Backend.API.Models.Responses;
using Backend.Business.Services;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    //[HttpPost]
    //public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    //{
    //    _logger.Information($"{request.UserName} {request.Password}");
    //    var id = _usersService.AddUser(new()
    //    {
    //        UserName = request.UserName,
    //        Password = request.Password,
    //        Email = request.Email,
    //        Age = request.Age,
    //    });

    //    return Ok(id);
    //}

    [HttpPost]
    public ActionResult<Guid> CreateUser(string userName, string password, string email, int age)
    {
        if (userName != null && password != null && email != null && age > 18 && age < 150)
        {
            return Ok(_usersService.CreateUser(userName, password, email, age));
        }

        return BadRequest();
    }

    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login([FromBody] LoginUserRequest user)
    {
        if (user is null)
        {
            return BadRequest("Invalid client request");
        }
        if (user.UserName == "johndoe" && user.Password == "def@123" && user.Email == "def@123")
        {
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
            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
        return Unauthorized();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute] Guid id, [FromBody] object request)
    {
        return NoContent();
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
}
