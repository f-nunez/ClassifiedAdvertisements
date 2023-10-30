using AngularWeb.Api.Settings;

internal static class AddBffTokenBasedAuthenticationServicesExtension
{
    public static void AddBffTokenBasedAuthenticationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var bffSettings = configuration
            .GetSection(nameof(BffTokenBasedAuthenticationSettings))
            .Get<BffTokenBasedAuthenticationSettings>();

        if (bffSettings is null)
            throw new ArgumentNullException(nameof(bffSettings), $"Required {nameof(bffSettings)}.");

        services.AddAuthentication(bffSettings.AuthenticationDefaultScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = bffSettings.JwtBearerAudience;

                options.Authority = bffSettings.JwtBearerAuthority;

                options.RequireHttpsMetadata = bffSettings.JwtBearerRequireHttpsMetadata;

                // Set custom claims instead of default values to get into ClaimsIdentity
                options.TokenValidationParameters.NameClaimType = bffSettings.JwtBearerTokenValidationParametersNameClaimType;
                options.TokenValidationParameters.RoleClaimType = bffSettings.JwtBearerTokenValidationParametersRoleClaimType;

                options.TokenValidationParameters.ValidateAudience = bffSettings.JwtBearerTokenValidationParametersValidateAudience;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("WebApiPolicy", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireClaim("scope", new string[] { "angular_web_api" });
            });

            options.AddPolicy("MyAdsPolicy", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireClaim("scope", new string[] { "angular_web_api" });
                policyBuilder.RequireRole(new string[] { "Customer", "Manager", "Staff" });
            });
        });
    }
}