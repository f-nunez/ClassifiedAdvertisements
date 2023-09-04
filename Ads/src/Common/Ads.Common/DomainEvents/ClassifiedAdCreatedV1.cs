namespace Ads.Common.DomainEvents;

public class ClassifiedAdCreatedV1 : BaseDomainEvent
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
}