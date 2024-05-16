namespace Backend.Core.Models.Coins.Responses;

public class OperationWithCoinsByCoinTypeResponse
{
    public Guid OperationId { get; set; }
    public int Quantity { get; set; }
    public Guid DeviceId { get; set; }
    public Guid OwnerId { get; set; }
}
