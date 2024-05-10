using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class CoinTypesWithQuantityResponse
{
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
}
