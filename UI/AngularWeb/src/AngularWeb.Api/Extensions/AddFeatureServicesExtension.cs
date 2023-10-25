using AngularWeb.Api.Services;

internal static class AddFeatureServicesExtension
{
    public static void AddFeatureServices(this IServiceCollection services)
    {
        services.AddTransient<IMyAdsService, MyAdsService>();
    }
}