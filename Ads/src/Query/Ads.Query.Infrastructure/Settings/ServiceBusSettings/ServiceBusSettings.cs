namespace Ads.Query.Infrastructure.Settings;

public class ServiceBusSettings
{
    public required string HostAddress { get; set; }
    public ServiceBusType ServiceBusType { get; set; }
}