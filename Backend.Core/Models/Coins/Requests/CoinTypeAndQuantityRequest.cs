using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class CoinTypeAndQuantityRequest
{
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
}
