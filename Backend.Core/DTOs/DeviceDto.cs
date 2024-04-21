using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class DeviceDto : IdContainer
{
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
    public UserDto Owner { get; set; }
}
