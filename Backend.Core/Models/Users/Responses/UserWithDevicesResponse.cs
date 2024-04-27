using Backend.Core.Models.Devices.Responses;

namespace Backend.Core.Models.Users.Responses;

public class UserWithDevicesResponse : UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
}
