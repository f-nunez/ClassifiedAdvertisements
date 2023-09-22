namespace Ads.Command.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public required Uri HostAddress { get; set; }
}