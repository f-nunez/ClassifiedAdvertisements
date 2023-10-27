using Ads.Command.Api.Middlewares;
using Ads.Command.Api.Services;
using Ads.Command.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy("CorsPolicy", corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();

                corsPolicyBuilder.AllowAnyMethod();

                corsPolicyBuilder.AllowAnyOrigin();
            });
        });

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}