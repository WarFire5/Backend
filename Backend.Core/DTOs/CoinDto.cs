using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class CoinDto : IdContainer
{
    public string CoinName { get; set; }
    public CoinType CoinType { get; set; }
    public string Quantity { get; set; }
    public UserDto Owner { get; set; }
}
