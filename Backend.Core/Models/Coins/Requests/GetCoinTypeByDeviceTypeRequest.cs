using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class GetCoinTypeByDeviceTypeRequest
{
    public DeviceType DeviceType { get; set; }
    public CoinType CoinType { get; set; }
}
