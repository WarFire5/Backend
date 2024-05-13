using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class CoinTypeAndQuantityByDeviceIdRequest
{
    public CoinType Type { get; set; }
    public int Quantity { get; set; }
    public Guid Id { get; set; }
}