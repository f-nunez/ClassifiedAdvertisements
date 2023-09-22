namespace Ads.Common.DomainEvents;

public class ClassifiedAdPublishedV1 : BaseDomainEvent
{
    public string? PublishedBy { get; set; }
    public DateTimeOffset PublishedOn { get; set; }
}