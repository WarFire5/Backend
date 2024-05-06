using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class DeviceDto : IdContainer
{
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public UserDto Owner { get; set; }
    public List<OperationWithCoinsDto> Coins { get; set; }
}
