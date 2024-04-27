using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Devices.Responses;
using Backend.Core.Models.Users.Responses;

namespace Backend.Core.Models.Coins;

public class CoinsMappingProfile : Profile
{
    public CoinsMappingProfile()
    {
        CreateMap<CoinDto, CoinResponse>();
    }
}
