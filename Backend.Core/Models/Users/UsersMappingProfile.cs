using AutoMapper;
using Backend.Core.DTOs;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Models.Users.Responses;

namespace Backend.Core.Models.Users;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<AddUserRequest, UserDto>();
        CreateMap<LoginUserRequest, UserDto>();
        CreateMap<UpdateUserRequest, UserDto>();

        CreateMap<UserDto, AuthenticatedResponse>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<UserDto, UserWithDevicesResponse>();
    }
}
