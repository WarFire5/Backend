using Backend.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Business;

public static class ConfigureServices
{
    public static void ConfigureBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesService>();
        services.AddScoped<ICoinsService, CoinsService>();
    }
}
