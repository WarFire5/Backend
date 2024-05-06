using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class OperationWithCoinsDto : IdContainer
{
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
    public DeviceDto Device { get; set; }
}
