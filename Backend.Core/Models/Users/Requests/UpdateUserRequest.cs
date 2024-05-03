namespace Backend.Core.Models.Users.Requests;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
