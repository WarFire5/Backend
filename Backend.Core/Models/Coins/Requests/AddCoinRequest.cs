using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Requests;

public class AddCoinRequest
{
    public CoinType CoinType { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Quantity { get; set; }
}
