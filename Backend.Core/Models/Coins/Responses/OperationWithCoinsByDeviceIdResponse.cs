using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class OperationWithCoinsByDeviceIdResponse
{
    public Guid OperationId { get; set; }
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
    public Guid OwnerId { get; set; }
}
