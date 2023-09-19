using Identity.Api.Settings;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.HttpLogging;
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

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders;
        });

        var cookiePolicySetting = configuration
            .GetSection(typeof(CookiePolicySetting).Name)
            .Get<CookiePolicySetting>()!;

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = cookiePolicySetting.MinimumSameSitePolicy;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddAuthentication();

        services.AddRazorPages();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseHttpsRedirection();
            Task.Run(() => SeedDataAsync(app));
        }

        app.UseHttpLogging();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityServer();

        app.UseCookiePolicy();

        app.UseAuthorization();

        app.MapRazorPages().RequireAuthorization();

        app.MapControllers();

        return app;
    }

    private static async void SeedDataAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var applicationDbSeeder = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContextSeeder>();

        await applicationDbSeeder.MigrateAsync();

        await applicationDbSeeder.SeedDataAsync();

        var configurationStoreDbSeeder = scope.ServiceProvider
            .GetRequiredService<ConfigurationStoreDbContextSeeder>();

        await configurationStoreDbSeeder.MigrateAsync();

        await configurationStoreDbSeeder.SeedDataAsync();

        var operationalStoreDbSeeder = scope.ServiceProvider
            .GetRequiredService<OperationalStoreDbContextSeeder>();

        await operationalStoreDbSeeder.MigrateAsync();
    }
}