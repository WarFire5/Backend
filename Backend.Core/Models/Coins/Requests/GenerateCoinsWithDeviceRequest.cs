using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class GenerateCoinsWithDeviceRequest
{
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
}
