namespace Ads.Common.DomainEvents;

public class ClassifiedAdUnpublishedV1 : BaseDomainEvent
{
    public string? UnpublishedBy { get; set; }
    public DateTimeOffset UnpublishedOn { get; set; }
}