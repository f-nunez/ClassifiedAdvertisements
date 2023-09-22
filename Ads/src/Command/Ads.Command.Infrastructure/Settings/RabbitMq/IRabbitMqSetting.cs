namespace Ads.Command.Infrastructure.Settings;

public interface IRabbitMqSetting
{
    Uri HostAddress { get; }
}