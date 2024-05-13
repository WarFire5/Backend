using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Requests;

public class DeviceRequest
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public Guid OwnerId { get; set; }
}
