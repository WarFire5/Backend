﻿namespace Backend.Core.Models.Users.Requests;

public class AddUserRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
