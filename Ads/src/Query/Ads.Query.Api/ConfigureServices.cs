using Ads.Query.Api.Middlewares;
using Ads.Query.Api.Services;
using Ads.Query.Api.Settings;
using Ads.Query.Application.Common.Interfaces;
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

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

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

        var authenticationSettings = configuration
            .GetSection(typeof(AuthenticationSettings).Name)
            .Get<AuthenticationSettings>();

        if (authenticationSettings is null)
            throw new ArgumentNullException(
                nameof(authenticationSettings), $"{nameof(authenticationSettings)} is required.");

        services.AddAuthentication(authenticationSettings.DefaultScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = authenticationSettings.Audience;

                options.Authority = authenticationSettings.Authority;

                options.RequireHttpsMetadata = authenticationSettings.RequireHttpsMetadata;

                options.TokenValidationParameters.ValidateAudience = authenticationSettings.TokenValidationParametersValidateAudience;
                options.TokenValidationParameters.ValidTypes = authenticationSettings.TokenValidationParametersValidTypes;
            });

        var authorizationSettings = configuration
            .GetSection(typeof(AuthorizationSettings).Name)
            .Get<AuthorizationSettings>();

        if (authorizationSettings is null)
            throw new ArgumentNullException(
                nameof(authorizationSettings), $"{nameof(authorizationSettings)} is required.");

        services.AddAuthorization(options =>
        {
            foreach (var policy in authorizationSettings.Policies)
                options.AddPolicy(policy.Name, policyBuilder =>
                {
                    if (policy.RequireAuthenticatedUser)
                        policyBuilder.RequireAuthenticatedUser();

                    if (policy.RequiredClaims is not null)
                        foreach (var requiredClaim in policy.RequiredClaims)
                            policyBuilder.RequireClaim(requiredClaim.ClaimType, requiredClaim.Values);

                    if (policy.RequiredRoles is not null)
                        policyBuilder.RequireRole(policy.RequiredRoles);
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

        app.UseCors(typeof(CorsPolicySettings).Name);

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("ApiPolicy");

        return app;
    }
}