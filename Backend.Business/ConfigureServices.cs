using Backend.Business.Services;
using Backend.Core.Models.Devices.Requests;
using Backend.Core.Models.Users.Requests;
using Backend.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Business;

public static class ConfigureServices
{
    public static void ConfigureBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesService>();
        services.AddScoped<ICoinsService, CoinsService>();

        services.AddScoped<IValidator<AddUserRequest>, AddUserValidator>();
        services.AddScoped<IValidator<DeviceRequest>, AddDeviceValidator>();
    }
}
