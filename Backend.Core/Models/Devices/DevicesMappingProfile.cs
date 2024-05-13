using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Coins.Requests;
using Backend.Core.Models.Devices.Requests;
using Backend.Core.Models.Devices.Responses;

namespace Backend.Core.Models.Devices;

public class DevicesMappingProfile : Profile
{
    public DevicesMappingProfile()
    {
        CreateMap<DeviceDto, DeviceResponse>();
        CreateMap<DeviceDto, ListDevicesResponse>();

        CreateMap<DeviceRequest, DeviceDto>();
        CreateMap<CoinTypeAndQuantityRequest, DeviceDto>();
    }
}
