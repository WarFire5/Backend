using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class GenerateCoinWithDeviceRequest
{
    public Guid DeviceId { get; set; }
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
}
