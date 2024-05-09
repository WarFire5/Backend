using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Coins.Responses;

namespace Backend.Core.Models.Coins;

public class CoinsMappingProfile : Profile
{
    public CoinsMappingProfile()
    {
        CreateMap<OperationWithCoinsDto, IdOperationWithCoinsResponse>();
        CreateMap<OperationWithCoinsDto, OperationWithCoinsResponse>()
            .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.Device.Id))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Device.Owner.Id));

        CreateMap<GetCoinTypeByDeviceTypeRequest, OperationWithCoinsDto>();
        CreateMap<GenerateCoinsWithDeviceRequest, OperationWithCoinsDto>();
    }
}
