using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class CoinTypeAndQuantityResponse
{
    public CoinType Type { get; set; }
    public int Quantity { get; set; }
}
