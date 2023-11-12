using AngularWeb.Api.Middlewares;
using AngularWeb.Api.Settings;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ShowPII only for development stages
        IdentityModelEventSource.ShowPII = true;

        // Needed when run behind a reverse proxy
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.AddHttpContextAccessor();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllersWithViews();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        var corsPolicySettings = configuration
            .GetSection(typeof(CorsPolicySettings).Name)
            .Get<CorsPolicySettings>();

        if (corsPolicySettings is null)
            throw new ArgumentNullException(
                nameof(corsPolicySettings), $"{nameof(corsPolicySettings)} is required.");

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(typeof(CorsPolicySettings).Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();

                corsPolicyBuilder.AllowAnyMethod();

                if (corsPolicySettings.WithOrigins is not null)
                    corsPolicyBuilder.WithOrigins(corsPolicySettings.WithOrigins);
                else
                    corsPolicyBuilder.AllowAnyOrigin();
            });
        });

        services.AddHttpClientServices(configuration);

        services.AddFeatureServices();

        services.AddBffCookieBasedAuthenticationServices(configuration);

        // services.AddBffTokenBasedAuthenticationServices(configuration);

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseMiddleware<HttpResponseExceptionHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors(typeof(CorsPolicySettings).Name);

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("WebApiPolicy");

        app.MapFallbackToFile("index.html");

        return app;
    }
}