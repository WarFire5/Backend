using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class CoinAndDeviceTypesRequest
{
    public CoinType CoinType { get; set; }
    public DeviceType DeviceType { get; set; }
}
