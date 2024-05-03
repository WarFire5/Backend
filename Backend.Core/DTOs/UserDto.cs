namespace Backend.Core.DTOs;

public class UserDto : IdContainer
{
    public string Login { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public List<DeviceDto> Devices { get; set; }
}
