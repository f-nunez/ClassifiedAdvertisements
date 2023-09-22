namespace Ads.Common.DomainEvents;

public class ClassifiedAdUpdatedV1 : BaseDomainEvent
{
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
}