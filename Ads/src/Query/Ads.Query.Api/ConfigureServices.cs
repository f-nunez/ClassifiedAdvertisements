using Ads.Query.Api.Filters;
using Ads.Query.Api.Services;
using Ads.Query.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}