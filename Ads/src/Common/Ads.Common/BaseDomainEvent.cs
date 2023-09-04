namespace Ads.Common;

public abstract class BaseDomainEvent
{
    public string? Id { get; set; }
    public long Version { get; set; }
}