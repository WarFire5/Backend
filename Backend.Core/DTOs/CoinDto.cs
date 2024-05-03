using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class CoinDto : IdContainer
{
    public CoinType CoinType { get; set; }
    public string Quantity { get; set; }
    public DeviceDto Device { get; set; }
}
