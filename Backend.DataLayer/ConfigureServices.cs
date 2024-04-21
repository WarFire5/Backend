using Backend.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.DataLayer;

public static class ConfigureServices
{
    public static void ConfigureDalServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDevicesRepository, DevicesRepository>();
    }
}
