using Backend.Core.Enums;

namespace Backend.Core.Models.Coins.Responses;

public class ListCoinTypesWithQuantityResponse
{
    public List<CoinTypesWithQuantityResponse> TypeQuantityList { get; set; }
}
