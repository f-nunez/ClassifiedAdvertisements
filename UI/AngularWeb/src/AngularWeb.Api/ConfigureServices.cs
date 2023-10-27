using AngularWeb.Api.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
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

        services.AddHttpClientServices(configuration);

        services.AddFeatureServices();

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

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}