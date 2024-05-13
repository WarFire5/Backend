using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class CoinTypeAndQuantityRequest
{
    public CoinType Type { get; set; }
    public int Quantity { get; set; }
}
