namespace Backend.API.Models.Requests;

public class LoginUserRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
