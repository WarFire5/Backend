using Backend.Core.Enums;
using Backend.DataLayer;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend.API.Extensions;

public static class DataBaseExtensions
{
    public static void ConfigureDataBase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("MwConnection");

        var dataSourceBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var dataSource = dataSourceBuilder.ConnectionString;

        services.AddDbContext<MainerWomanContext>(
            options => options
                .UseNpgsql(dataSource)
                .UseSnakeCaseNamingConvention()
        );

        NpgsqlConnection.GlobalTypeMapper.MapEnum<CoinType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<DeviceType>();
    }


    public static async Task MigrateAndReloadPostgresTypesAsync(this IServiceProvider serviceProvider, CancellationToken token = default)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MainerWomanContext>();

            await dbContext.Database.MigrateAsync(token);

            if (dbContext.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
            {
                await npgsqlConnection.OpenAsync(token);

                try
                {
                    await npgsqlConnection.ReloadTypesAsync();
                }
                finally
                {
                    await npgsqlConnection.CloseAsync();
                }
            }
        }
    }
}
