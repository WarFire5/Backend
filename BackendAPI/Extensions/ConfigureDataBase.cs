using Microsoft.EntityFrameworkCore;
using Backend.DataLayer;

namespace Backend.API.Extensions;

public static class DataBaseExtensions
{
    public static void ConfigureDataBase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<MainerWomanContext>(
            options => options
            .UseNpgsql(configurationManager.GetConnectionString("MwConnection"))
            .UseSnakeCaseNamingConvention()
        );
    }
}
