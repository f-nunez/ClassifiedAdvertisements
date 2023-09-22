namespace Ads.Query.Infrastructure.Settings;

public interface IRabbitMqSetting
{
    Uri HostAddress { get; }
}