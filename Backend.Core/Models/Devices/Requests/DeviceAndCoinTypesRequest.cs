using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Requests;

public class DeviceAndCoinTypesRequest
{
    public DeviceType DeviceType { get; set; }
    public CoinType CoinType { get; set; }
}
