using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;

namespace Backend.Core.Models.Coins;

public class CoinsMappingProfile : Profile
{
    public CoinsMappingProfile()
    {
        CreateMap<OperationWithCoinsDto, CoinIdResponse>();

        CreateMap<GetCoinTypeByDeviceTypeRequest, OperationWithCoinsDto>();
        CreateMap<GenerateCoinWithDeviceRequest, OperationWithCoinsDto>();
    }
}
