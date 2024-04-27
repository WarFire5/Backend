using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Devices.Responses;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;

namespace Backend.Core.Models.Devices;

public class DevicesMappingProfile : Profile
{
    public DevicesMappingProfile()
    {
        CreateMap<DeviceDto, DeviceResponse>();
        CreateMap<DeviceDto, UserWithDevicesResponse>();
    }
}
