using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class OperationWithCoinsResponse
{
    public Guid OperationId { get; set; }
    public CoinType Type { get; set; }
    public int Quantity { get; set; }
    public Guid DeviceId { get; set; }
    public Guid OwnerId { get; set; }
}
