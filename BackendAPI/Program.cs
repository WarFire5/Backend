using Backend.API.Extensions;
using Backend.Business;
using Backend.Core.Exceptions;
using Backend.DataLayer;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.ConfigureApiServices();
        builder.Services.ConfigureBllServices();
        builder.Services.ConfigureDataBase(builder.Configuration);
        builder.Services.ConfigureDalServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    var exception = exceptionHandlerFeature.Error;

                    if (exception is NotFoundException notFoundException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsJsonAsync(new { error = notFoundException.Message });
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsJsonAsync(new { error = "¬нутренн€€ ошибка сервера" });
                    }
                }
            });
        });

        app.Run();
    }
}