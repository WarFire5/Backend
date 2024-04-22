namespace Backend.Core.DTOs;

public class UserDto : IdContainer
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public List<DeviceDto> Devices { get; set; }
    public List<CoinDto> Coins { get; set; }
}
