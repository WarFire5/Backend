using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string DeviceName { get; set; }
    public DeviceType DeviceType { get; set; }
    public Guid OwnerId { get; set; }
}
