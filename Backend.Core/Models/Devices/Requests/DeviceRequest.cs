using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Requests;

public class DeviceRequest
{
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public Guid OwnerId { get; set; }
}
