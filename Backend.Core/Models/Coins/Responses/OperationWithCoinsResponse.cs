using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class OperationWithCoinsResponse
{
    public Guid Id { get; set; }
    public CoinType CoinType { get; set; }
    public int Quantity { get; set; }
    public Guid DeviceId { get; set; }
    public Guid OwnerId { get; set; }
}
