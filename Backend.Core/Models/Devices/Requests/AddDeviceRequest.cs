using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Requests;

public class AddDeviceRequest
{
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public UserDto Owner { get; set; }
}
