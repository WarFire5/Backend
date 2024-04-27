using Backend.Core.Enums;

namespace Backend.Core.Models.Devices.Responses;

public class CoinResponse
{
    public Guid Id { get; set; }
    public string CoinName { get; set; }
    public CoinType CoinType { get; set; }
    public string Quantity { get; set; }
}