using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class CoinTypeAndQuantityByOwnerIdRequest
{
    public CoinType Type { get; set; }
    public int Quantity { get; set; }
    public Guid Id { get; set; }
}