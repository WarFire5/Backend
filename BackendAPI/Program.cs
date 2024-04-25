using Backend.API.Configuration;
using Backend.API.Extensions;
using Backend.Business;
using Backend.DataLayer;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // Add services to the container.
    builder.Services.ConfigureApiServices();
    builder.Services.ConfigureBllServices();
    builder.Services.ConfigureDataBase(builder.Configuration);
    builder.Services.ConfigureDalServices();

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Running up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex.Message);
}
finally
{
    Log.Information("App stopped.");
    Log.CloseAndFlush();
}