namespace Ads.Query.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public required Uri HostAddress { get; set; }
}